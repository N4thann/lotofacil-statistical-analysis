using Lotofacil.Application.Services.Interfaces;
using Lotofacil.Domain.Entities;
using Lotofacil.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lotofacil.Application.Services
{
    public class ContestActivityLogService : IContestActivityLogService
    {
        private readonly IBaseContestRepository<ContestActivityLog> _repository;
        public ContestActivityLogService(IBaseContestRepository<ContestActivityLog> repository)
        {
            _repository = repository;
        }
        public IQueryable<ContestActivityLog> GetQueryableContestActivityLogs()
        {
            return _repository.GetAllQueryable();
        }

        public async Task<List<ContestActivityLog>> GetFilteredContestActivityLogsAsync(string? name, DateTime? startDate, DateTime? endDate, int pageNumber, int pageSize)
        {
            var query = GetQueryableContestActivityLogs();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(log => log.Name.Contains(name));

            if (startDate.HasValue)
                query = query.Where(log => log.Data >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(log => log.Data <= endDate.Value);

            return await query
                .OrderBy(log => log.Data)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> CountFilteredContestActivityLogsAsync(string? name, DateTime? startDate, DateTime? endDate)
        {
            var query = GetQueryableContestActivityLogs();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(log => log.Name.Contains(name));

            if (startDate.HasValue)
                query = query.Where(log => log.Data >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(log => log.Data <= endDate.Value);

            return await query.CountAsync();
        }

    }
}
