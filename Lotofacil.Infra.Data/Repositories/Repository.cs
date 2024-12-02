using Lotofacil.Domain.Entities;
using Lotofacil.Domain.Interfaces;
using Lotofacil.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Lotofacil.Infra.Data.Repositories
{
    public class Repository<T> : IBaseContestRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context) => _context = context;
        

        public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);

        //Mesmo que esse método seja para uma listagem em uma tabela em que cada registro tenha uma opção
        //de edição, o AsNoTracking ainda irá otimizar apenas para o carregamento da tabela 
        //e posteriormente posso fazer uma procura sem o AsNoTracking para procurar o registro com GetByIdAsync       
        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>()
                .AsNoTracking().ToListAsync();

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
                throw new KeyNotFoundException($"Entidade com ID {id} não encontrada.");

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        /*Por que GetAllQueryable não precisa ser assíncrono?
        IQueryable representa apenas a definição da consulta e não a execução dela.
        Ele não faz nenhuma operação no banco até que seja "materializado" (com ToList, First, etc.).
        Tornar o método assíncrono criaria complexidade desnecessária sem ganho real.
        O compilador não permite o uso de await para um retorno que já é materializado de forma síncrona.
         * 
         * */
        public IQueryable<T> GetAllQueryable()
        {
            return _context.Set<T>().AsQueryable();
        }
    }
}
