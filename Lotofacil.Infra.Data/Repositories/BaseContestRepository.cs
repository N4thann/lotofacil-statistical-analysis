using Lotofacil.Domain.Entities;
using Lotofacil.Domain.Interfaces;
using Lotofacil.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Lotofacil.Infra.Data.Repositories
{
    /*Ao herdar de um repositório genérico (Repository<T>), você evita duplicar implementações de métodos CRUD básicos, 
     * como AddAsync, UpdateAsync, DeleteAsync e GetAllAsync. 
     * Esses métodos são comuns a todas as entidades e podem ser definidos no repositório genérico.*/
    public class BaseContestRepository : IBaseContestRepository
    {
        private readonly ApplicationDbContext _context;

        /*O : base(context) é usado quando uma classe herda de outra classe e deseja passar argumentos para o construtor da classe base. 
         * Isso é comum quando criamos repositórios que herdam de um repositório genérico.*/
        public BaseContestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BaseContest>> GetAllWithContestsAbove11Async()
        {
            return await _context.BaseContests
                .Include(bc => bc.ContestsAbove11)
                .OrderBy(bc => bc.CreatedAt)
                .AsSplitQuery()
                .ToListAsync();
        }
        public async Task UpdateBaseContestAsync(BaseContest baseContest)
        {
            _context.BaseContests.Update(baseContest);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string contestName)
        {
            return await _context.BaseContests.AnyAsync(c => c.Name == contestName);
        }
        /// <summary>
        /// Estou utilizando nesse método o conceito do Explicit Loading.
        /// Se não quiser Include() em tudo, mas também não quiser deixar consultas automáticas descontroladas (Lazy Loading), 
        /// use Explicit Loading. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BaseContest> GetByIdAsync(int id)
        {
            var baseContest = await _context.BaseContests.FindAsync(id);
            if (baseContest != null)
            {
                await _context.Entry(baseContest)
                    .Collection(bc => bc.ContestsAbove11)
                    .LoadAsync();
            }
            return baseContest;
        }
    }
}
/*Se quer simplicidade e não se preocupa com consultas extras → Ative o Lazy Loading.
Se quer controle e evitar consultas desnecessárias → Use Include() ou LoadAsync() manualmente.
Se tem problemas de desempenho por muitas consultas pequenas → Desative Lazy Loading e sempre use Include().
👉 Minha sugestão: Se está começando, não ative o Lazy Loading agora. Prefira Include() e vá ativando conforme precisar.
 */
