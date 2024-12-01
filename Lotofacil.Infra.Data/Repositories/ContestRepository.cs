using Lotofacil.Domain.Entities;
using Lotofacil.Domain.Interfaces;
using Lotofacil.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotofacil.Infra.Data.Repositories
{
    public class ContestRepository : Repository<Contest>, IContestRepository
    {
        private readonly ApplicationDbContext _context;

        public ContestRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contest>> GetAllWithBaseContestsAsync()
        {
            return await _context.Contests
                .Include(bc => bc.BaseContests)
                .ToListAsync();
        }
    }
}
