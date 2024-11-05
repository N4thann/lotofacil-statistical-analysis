using Lotofacil.Infra.Data.Context;
using Lotofacil.Application.ViewsModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Lotofacil.Domain.Entities;
using Lotofacil.Application.Services.Interfaces;

namespace Lotofacil.Controllers
{
    public class ContestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IContestManagementService _managementService;

        public ContestController(ApplicationDbContext context, IContestManagementService managementService)
        {
            _context = context;
            _managementService = managementService;
        }

        public IActionResult List()
        {
            try
            {
                var contest = _context.Contests
                .OrderByDescending(c => c.Data)
                .AsNoTracking()
                .ToList();

                return contest.Any()
                    ? View(contest)
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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateContestViewModel baseContest)
        {
            if (ModelState.IsValid)
            {
                baseContest.Numbers = _managementService.FormatNumbersToSave(baseContest.Numbers);
                BaseContest contest = new BaseContest(baseContest.Name, baseContest.Data, baseContest.Numbers);

                _context.Add(contest);
                await _context.SaveChangesAsync();
                return RedirectToAction("List", "Contest");
            }
            return View(baseContest);
        }

    }
}
