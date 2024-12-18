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
        private readonly IContestManagementService _contestMS;
        private readonly IJobHandler _jobHandler;

        public HomeController(ApplicationDbContext context, IBaseContestService baseContestService,
            ILogger<HomeController> logger, IJobHandler jobHandler, IContestManagementService contestMS)
        {
            _context = context;
            _logger = logger;
            _baseContestService = baseContestService;
            _jobHandler = jobHandler;
            _contestMS = contestMS;
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

        [HttpGet()]
        public async Task<IActionResult> Dash()
        {
            try
            { 
                var baseContest = await _baseContestService.GetAllWithContestsAbove11Async();

                // Calcula o somatório para cada concurso
                var contestsWithSum = baseContest
                    .Select(x => new
                    {
                        Contest = x,
                        Sum = (x.Hit11 * 1) + (x.Hit12 * 2) + (x.Hit13 * 3) + (x.Hit14 * 4) + (x.Hit15 * 5)
                    })
                    .ToList();

                // Ordena pelo maior somatório e pega os dois primeiros
                var topTwoContests = contestsWithSum
                    .OrderByDescending(x => x.Sum)
                    .Take(2)
                    .Select(x => x.Contest)
                    .ToList();

                // Lista para armazenar os ViewModels
                var viewModel = new List<TopContestViewModel>();

                // Processa os dois maiores concursos
                foreach (var x in topTwoContests)
                {
                    // Dicionário para contar as ocorrências dos números (1 a 25)
                    var occurrences = new Dictionary<int, int>();
                    for (int i = 1; i <= 25; i++)
                    {
                        occurrences[i] = 0;
                    }

                    // Calcula as ocorrências
                    foreach (var y in x.ContestsAbove11)
                    {
                        var numbers = _contestMS.ConvertFormattedStringToList(y.Numbers);

                        foreach (var i in numbers)
                        {
                            if (occurrences.ContainsKey(i))
                            {
                                occurrences[i]++;
                            }
                        }
                    }

                    // Cria o ViewModel para o concurso
                    viewModel.Add(new TopContestViewModel
                    {
                        Name = x.Name,
                        Data = x.Data,
                        Number = x.Numbers,
                        NumberOccurences = occurrences.Select(o => new NumberOccurencesViewModel
                        {
                            Number = o.Key,
                            Occurences = o.Value
                        }).ToList()
                    });
                }

                return View(viewModel);
            }

            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel("Erro ao calcular os 2 concursos base com mais acertos.",
                    ex.Message, 4));
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}