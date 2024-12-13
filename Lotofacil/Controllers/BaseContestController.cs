 using Lotofacil.Application.ViewsModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lotofacil.Application.Services.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Lotofacil.Presentation.Extensions;

namespace Lotofacil.Presentation.Controllers
{
    public class BaseContestController : Controller
    {
        private readonly IBaseContestService _baseContestService;
        private readonly IValidator<ContestViewModel> _validator;

        public BaseContestController(IBaseContestService baseContestService,
            IValidator<ContestViewModel> validator)
        {
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
        public async Task<IActionResult> Create(ContestViewModel baseContestVM)
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
        /*
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
        */

        [HttpGet()]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _baseContestService.ShowOnScreen(id));
        }

        [HttpPost()]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ContestViewModel baseContestVM)
        {
            ValidationResult result = await _validator.ValidateAsync(baseContestVM);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                return View("Edit", baseContestVM);
            }

            try
            {
                await _baseContestService.EditBaseContestAsync(baseContestVM);
                TempData["notice"] = "Concurso Base Editado com Sucesso!";
                return RedirectToAction("List", "BaseContest");

            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel("Erro ao editar o registro.",
                    ex.Message, 1));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var contestViewModel = await _baseContestService.ShowOnScreen(id);
            return View(contestViewModel);
        }

        [HttpPost(), ActionName("Delete")]//Quando for chamado esse método tem que chamar por "delete" graças ao ActionName
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _baseContestService.DeleteByIdAsync(id);
                TempData["notice"] = "Concurso Base deletado com Sucesso!";
                return RedirectToAction("List", "BaseContest");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel("Erro ao deletar o resgistro.",
                    ex.Message, 1));
            }          
        }
    }
}
