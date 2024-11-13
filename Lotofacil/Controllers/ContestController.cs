using Lotofacil.Infra.Data.Context;
using Lotofacil.Application.ViewsModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lotofacil.Domain.Entities;
using Lotofacil.Application.Services.Interfaces;
using FluentValidation.Results;
using FluentValidation;
using Lotofacil.Presentation.Extensions;

namespace Lotofacil.Presentation.Controllers
{
    public class ContestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IContestManagementService _managementService;
        private readonly IValidator<CreateContestViewModel> _validator;
        private readonly IContestService _contestService;

        public ContestController(ApplicationDbContext context, 
            IContestManagementService managementService,
            IValidator<CreateContestViewModel> validator,
            IContestService contestService)
        {
            _context = context;
            _managementService = managementService;
            _validator = validator;
            _contestService = contestService;
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
        public async Task<IActionResult> Create(CreateContestViewModel contestVM)
        {
            ValidationResult result = await _validator.ValidateAsync(contestVM);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                return View("Create", contestVM);
            }
            //Dentro desse serviço é transformado em um objeto do tipo Contest para salvar no banco
            await _contestService.CreateAsync(contestVM);
            TempData["notice"] = "Concurso Criado com Sucesso!";
            return RedirectToAction("List", "Contest");
        }

    }
}
