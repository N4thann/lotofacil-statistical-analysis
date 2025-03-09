using Lotofacil.Infra.Data.Context;
using Lotofacil.Application.ViewsModel;
using Microsoft.AspNetCore.Mvc;
using Lotofacil.Application.Services.Interfaces;
using Lotofacil.Application.Services;
using Lotofacil.Domain.Entities;
using Lotofacil.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;

namespace Lotofacil.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IBaseContestService _baseContestService;
        private readonly IContestManagementService _contestMS;
        private readonly IRepository<Contest> _repository;

        public HomeController(ApplicationDbContext context, IBaseContestService baseContestService,
            ILogger<HomeController> logger, IContestManagementService contestMS,
            IRepository<Contest> repository)
        {
            _context = context;
            _logger = logger;
            _baseContestService = baseContestService;
            _contestMS = contestMS;
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Obtém a lista de Concursos Base
                var baseContestList = await _baseContestService.GetAllBaseContestAsync();

                if (!baseContestList.Any())
                {
                    return View("Error", new ErrorViewModel(
                        "Nenhum registro encontrado na tabela Contests.", null, 2)); // Código para ErrorType.NoRecords
                }

                var orderedBaseContestList = baseContestList
                    .OrderByDescending(x => (x.Hit11 * 1) + (x.Hit12 * 2) + (x.Hit13 * 3) + (x.Hit14 * 4) + (x.Hit15 * 5))//Cálculo para medir a eficiência de um concurso base
                    .ToList();

                return View(orderedBaseContestList);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel(
                    "Erro ao acessar os dados do banco de dados da tabela Contests.",
                    ex.Message, 1));
            }
        }

        public async Task<IActionResult> Dash()
        {
            try
            { 
                var baseContest = await _baseContestService.GetAllWithContestsAbove11Async();

                if (!baseContest.Any())
                {
                    return View("Error", new ErrorViewModel(
                        "Nenhum registro encontrado na tabela Contests.", null, 2)); // Código para ErrorType.NoRecords
                }

                var viewModel =  _contestMS.TopTwoContests(baseContest);

                return View(viewModel);
            }

            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel("Erro ao calcular os 2 concursos base com mais acertos.",
                    ex.Message, 4));
            }
        }

        public async Task<IActionResult> Dash2(string? name, DateTime? startDate, DateTime? endDate, int page = 1, int pageSize = 10)
        {
            var baseContests = await _baseContestService.GetFilteredBaseContestsAsync(name, startDate, endDate, page, pageSize);
            var totalCount = await _baseContestService.GetTotalCountAsync(name, startDate, endDate); // Implementação no serviço para pegar o total de registros

            var model = new PagedResultViewModel<BaseContest>
            {
                Datas = baseContests,
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
            var baseContests = await _baseContestService.GetFilteredBaseContestsAsync(name, startDate, endDate, 1, int.MaxValue); // Pega todos os registros

            var stream = _contestMS.GenerateExcelForBaseContest(baseContests);

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"BaseContest_{DateTime.Now:dd-MM-yyyy}.xlsx");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Dash3()
        {
            var dash3 = new Dash3ViewModel();

            var baseContests = await _baseContestService.GetAllBaseContestAsync();

            var contests = await _repository.GetAllAsync();

            var lastContest = contests.LastOrDefault();
            if (lastContest != null)
            {
                dash3.LastContest = lastContest.Name;
            }

            var firstContest = contests.FirstOrDefault();
            if (firstContest != null)
            {
                dash3.FirstContest = firstContest.Name; 
            }

            dash3.Years = contests
                .GroupBy(c => c.Data.Year)
                .OrderBy(g => g.Key)
                .ToDictionary(g => g.Key.ToString(), g => g.Count());

            dash3.TotalBaseContests = baseContests.Count();

            dash3.TotalContests = contests.Count();

            return View(dash3);
        }
    }
}