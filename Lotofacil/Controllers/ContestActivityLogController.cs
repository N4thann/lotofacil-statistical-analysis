using Lotofacil.Application.Services;
using Lotofacil.Application.Services.Interfaces;
using Lotofacil.Application.ViewsModel;
using Lotofacil.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lotofacil.Web.Controllers
{
    public class ContestActivityLogController : Controller
    {
        private readonly IContestActivityLogService _contestActivityLogService;
        private readonly IContestManagementService _contestMS;

        public ContestActivityLogController(IContestActivityLogService contestActivityLogService,
            IContestManagementService contestMS) 
        {
            _contestActivityLogService = contestActivityLogService;
            _contestMS = contestMS;
        }

        // Exibe a View com os filtros e os dados
        public async Task<IActionResult> Index(string? name, DateTime? startDate, DateTime? endDate, int page = 1, int pageSize = 10)
        {
            var logs = await _contestActivityLogService.GetFilteredContestActivityLogsAsync(name, startDate, endDate, page, pageSize);
            var totalCount = await _contestActivityLogService.GetTotalCountAsync(name, startDate, endDate); // Implementação no serviço para pegar o total de registros

            var model = new PagedResultViewModel
            {
                Logs = logs,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize),
                NameFilter = name,
                StartDateFilter = startDate,
                EndDateFilter = endDate
            };

            return View(model);
        }

        public async Task<IActionResult> ExportToExcel(string? name, DateTime? startDate, DateTime? endDate)
        {
            var logs = await _contestActivityLogService.GetFilteredContestActivityLogsAsync(name, startDate, endDate, 1, int.MaxValue); // Pega todos os registros

            var stream = _contestMS.GenerateExcelContestActivityLog(logs); // Método utilitário para gerar o Excel

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ContestActivityLogs.xlsx");
        }
    }
}
