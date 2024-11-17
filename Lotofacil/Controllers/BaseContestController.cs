using Lotofacil.Infra.Data.Context;
using Lotofacil.Domain.Entities;
using Lotofacil.Application.ViewsModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lotofacil.Application.Services.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Lotofacil.Presentation.Extensions;
using System.Runtime.InteropServices;

namespace Lotofacil.Presentation.Controllers
{
    public class BaseContestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBaseContestService _baseContestService;
        private readonly IValidator<CreateContestViewModel> _validator;

        public BaseContestController(ApplicationDbContext context, 
            IBaseContestService baseContestService,
            IValidator<CreateContestViewModel> validator)
        {
            _context = context;
            _baseContestService = baseContestService;
            _validator = validator;
        }

        [HttpGet()]
        public async Task<IActionResult> List()
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateContestViewModel baseContestVM)
        {
            ValidationResult result = await _validator.ValidateAsync(baseContestVM);

            if(!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                return View("Create", baseContestVM);
            }

            await _baseContestService.CreateAsync(baseContestVM);
            TempData["notice"] = "Concurso Base Criado com Sucesso!";
            return RedirectToAction("List", "BaseContest");
        }

        public async Task<IActionResult> Details(string contestName)
        {
            if (string.IsNullOrEmpty(contestName))
            {
                return View();
            }

            var baseContest = await _context.BaseContests
            .Include(cb => cb.ContestsAbove11)
            .OrderBy(cb => cb.Name)
            .ThenBy(cb => cb.Id)
            .AsSplitQuery()
            .FirstOrDefaultAsync(cb => cb.Name.ToLower() == contestName.ToLower());

            if (baseContest == null)
            {
                ViewBag.ErrorMessage = $"Concurso Base contendo '{contestName}' não foi encontrado.";
                return View("Error");
            }

            return View(baseContest);
        }

        [HttpGet()]
        public async Task<IActionResult> Edit(int? id)
        {
            var baseContest = _context.BaseContests
                .FirstOrDefault(g => g.Id == id);

            if (id == null || baseContest == null)
            {
                return View("Error", new ErrorViewModel(
                    "Recurso não encontrado.", null, 3));
            }

            CreateContestViewModel baseContestVM = new CreateContestViewModel();
            baseContestVM.Name = baseContest.Name;
            baseContestVM.Data = baseContest.Data;
            baseContestVM.Numbers = baseContest.Numbers;

            return View(baseContestVM);
        }

        [HttpPost()]
        public async Task<IActionResult> Edit(int id, CreateContestViewModel baseContestVM)
        {
            ValidationResult result = await _validator.ValidateAsync(baseContestVM);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                return View("Edit", baseContestVM);
            }

            await _baseContestService.EditBaseContestAsync(id, baseContestVM);
            TempData["notice"] = "Concurso Base Editado com Sucesso!";
            return RedirectToAction("List", "BaseContest");
        }

        [HttpGet()]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null) return View("Error", "Id nulo");

            var baseContest = _context.BaseContests.FirstOrDefault(c => c.Id == id);

            if (baseContest == null) return View("Error", "Nenhum concurso encontrado.");

            return View(baseContest);
        }

        [HttpPost(), ActionName("Delete")]//Quando for chamado esse método tem que chamar por "delete" graças ao ActionName
        public ActionResult DeleteConfirmed(int id)
        {
            var contest = _context.BaseContests.FirstOrDefault(c => c.Id == id);

            if (contest is null) return View("Error", "Nenhum concurso encontrado.");

            _context.BaseContests.Remove(contest);
            _context.SaveChanges();

            return RedirectToAction("List");
        }

        private bool BaseContestExists(int id)
        {
            return (_context.BaseContests?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
