using Lotofacil.Domain.Entities;
using Lotofacil.Domain.Interfaces;
using Lotofacil.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Lotofacil.Infra.Data.Repositories
{
    public class ContestRepository : IContestRepository
    {
        private readonly ApplicationDbContext _context;

        public ContestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contest>> GetAllWithBaseContestsAsync()
        {
            return await _context.Contests
                .Include(bc => bc.BaseContests)
                .OrderBy(bc => bc.Data)
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<IEnumerable<Contest>> GetAllWithBaseContestsOrderedAsync()
        {
            return await _context.Contests
                .Include(bc => bc.BaseContests)
                .OrderBy(bc => bc.LastProcessed)
                .ToListAsync();
        }

        public async Task UpdateContestAsync(Contest contest)
        {
            _context.Contests.Update(contest);
            await _context.SaveChangesAsync();
        }


    }
}
