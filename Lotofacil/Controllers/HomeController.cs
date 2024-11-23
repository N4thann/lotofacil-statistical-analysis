using Lotofacil.Infra.Data.Context;
using Lotofacil.Application.ViewsModel;
using Microsoft.AspNetCore.Mvc;
using Lotofacil.Application.Services.Interfaces;

namespace Lotofacil.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IBaseContestService _baseContestService;
        private readonly IJobHandler _jobHandler;

        public HomeController(ApplicationDbContext context, IBaseContestService baseContestService,
            ILogger<HomeController> logger, IJobHandler jobHandler)
        {
            _context = context;
            _logger = logger;
            _baseContestService = baseContestService;
            _jobHandler = jobHandler;
        }

        [HttpGet()]
        public async Task<IActionResult> Index()
        {
            try
            {
                var baseContestList = await _baseContestService.GetAllBaseContestAsync();

                return baseContestList.Any()
                     ? View(baseContestList)
                     : View("Error", new ErrorViewModel(
                     "Nenhum registro encontrado na tabela Contests.", null, 2) // Código que representa ErrorType.NoRecords
                     );
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel("Erro ao acessar os dados do banco de dados da tabela Contests.",
                    ex.Message, 1));
            }
        }
        [HttpPost]
        public async Task<IActionResult> ExecuteJobManually()
        {
            try
            {
                await _jobHandler.ExecuteAsync();
                TempData["Message"] = "Job executado com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Erro ao executar o job: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}