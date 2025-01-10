using DocumentFormat.OpenXml.Office2010.Excel;
using Lotofacil.Application.Services.Interfaces;
using Lotofacil.Domain.Entities;
using Lotofacil.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Lotofacil.Application.Services
{
    public class ContestActivityLogService : IContestActivityLogService
    {
        private readonly IRepository<ContestActivityLog> _repository;
        public ContestActivityLogService(IRepository<ContestActivityLog> repository)
        {
            _repository = repository;
        }
        public IQueryable<ContestActivityLog> GetQueryableContestActivityLogs()
        {
            return _repository.GetAllQueryable();
        }

        public async Task DeleteAllReferencesOfLogByBaseContest(string baseContestName)
        {
            var logs = await _repository.GetAllAsync();
            foreach (var log in logs)
            {
                if(log.BaseContestName.Contains(baseContestName))
                {
                    await _repository.DeleteAsync(log.Id);
                }
            }
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
                .OrderByDescending(log => log.CreateTime)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(string? name, DateTime? startDate, DateTime? endDate)
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
