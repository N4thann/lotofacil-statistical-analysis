using Lotofacil.Infra.Data.Context;
using Lotofacil.Domain.Entities;
using Lotofacil.Application.ViewsModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Lotofacil.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dash1()
        {
            try
            {
                var baseContests = _context.BaseContests
                    .Include(cb => cb.ContestsAbove11)
                    .OrderBy(cb => cb.Name)
                    .ThenBy(cb => cb.Id)
                    .AsSplitQuery()
                    .ToList();

                if (baseContests == null || !baseContests.Any())
                {
                    return NotFound("Nenhum concurso encontrado.");
                }

                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                return View("Error", new ErrorViewModel("Erro ao acessar os dados do banco de dados da tabela Contests.",
                    ex.Message, 1));
            }
            
        }

        private List<int> GetContestNumbers(string numbers)
        {
            if (string.IsNullOrWhiteSpace(numbers) || numbers.Length % 2 != 0)
                return new List<int>();

            var NumbersList = new List<int>();

            for (int i = 0; i < numbers.Length; i += 2)
            {
                if (i + 1 < numbers.Length) // Verifica se existe um segundo caractere
                {
                    var strNumber = numbers.Substring(i, 2);

                    // Usa TryParse para evitar exceções
                    if (int.TryParse(strNumber, out int number))
                    {
                        NumbersList.Add(number);
                    }
                    else
                    {
                        throw new FormatException($"Número inválido encontrado: {strNumber}");
                    }
                }
            }
            return NumbersList;
        }
        private List<int> GetBaseContestNumbers(string numbers)
        {
            if (string.IsNullOrWhiteSpace(numbers))
                return new List<int>();

            // Divide a string pelos hífens e tenta converter cada parte para um inteiro
            var numbersList = new List<int>();
            var parts = numbers.Split(',');

            foreach (var part in parts)
            {
                // Tenta converter a parte em inteiro
                if (int.TryParse(part.Trim(), out int number))
                {
                    numbersList.Add(number);
                }
                else
                {
                    throw new FormatException($"Número inválido encontrado: {part}");
                }
            }

            return numbersList;
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}