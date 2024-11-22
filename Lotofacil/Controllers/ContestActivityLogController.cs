using Lotofacil.Application.Services.Interfaces;
using Lotofacil.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lotofacil.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContestActivityLogController : ControllerBase
    {
        private readonly IContestActivityLogService _contestActivityLogService;
        private readonly IContestManagementService _contestMS;

        public ContestActivityLogController(IContestActivityLogService contestActivityLogService,
            IContestManagementService contestMS) 
        {
            _contestActivityLogService = contestActivityLogService;
            _contestMS = contestMS;
        }

            [HttpGet("GetContestActivityLogs")]
            public async Task<IActionResult> GetContestActivityLogs(
                string? name,
                DateTime? startDate,
                DateTime? endDate,
                int pageNumber = 1,
                int pageSize = 10)
            {
                var totalRecords = await _contestActivityLogService.CountFilteredContestActivityLogsAsync(name, startDate, endDate);
                var logs = await _contestActivityLogService.GetFilteredContestActivityLogsAsync(name, startDate, endDate, pageNumber, pageSize);

                return Ok(new
                {
                    TotalRecords = totalRecords,
                    Logs = logs
                });
            }

            [HttpGet("ExportToExcel")]
            public IActionResult ExportToExcel(string? name, DateTime? startDate, DateTime? endDate)
            {
                var query = _contestActivityLogService.GetQueryableContestActivityLogs();

                if (!string.IsNullOrEmpty(name))
                    query = query.Where(log => log.Name.Contains(name));
                if (startDate.HasValue)
                    query = query.Where(log => log.Data >= startDate.Value);
                if (endDate.HasValue)
                    query = query.Where(log => log.Data <= endDate.Value);

                var logs = query.ToList(); // Pode ser otimizado se filtrar menos dados
                var memoryStream = _contestMS.GenerateExcelContestActivityLog(logs);

                return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ContestActivityLogs.xlsx");
            }
        }
}
