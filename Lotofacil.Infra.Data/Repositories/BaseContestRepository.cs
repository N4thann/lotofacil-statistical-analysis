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
    }
}
