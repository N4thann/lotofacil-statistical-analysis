using Lotofacil.Infra.Data.Context;
using Lotofacil.Domain.Entities;
using Lotofacil.Application.ViewsModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;

namespace Lotofacil.Controllers
{
    public class BaseContestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BaseContestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public async Task<IActionResult> List()
        {
            try
            {
                var baseContests =  await _context.BaseContests
                    .OrderByDescending(cb => cb.Data)
                    .ThenBy(cb => cb.Name)
                    .ToListAsync();

                return baseContests.Any()
                    ? View(baseContests)
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
        public async Task<IActionResult> Create(CreateBaseContestViewModel baseContest)
        {
            if (ModelState.IsValid)
            {
                BaseContest contest = new BaseContest(baseContest.Id, baseContest.Name, baseContest.Data, baseContest.BaseContestName);

                _context.Add(contest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(baseContest);
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
                return NotFound();
            }

            if (baseContest == null)
            {
                return View("Error", "Nenhum concurso base encontrado.");
            }

            return View(baseContest);
        }

        [HttpPost()]
        public async Task<IActionResult> Edit(int id, Contest concurso)
        {
            ModelState.Remove("ConcursosAcimaDe11");
            if (ModelState.IsValid)
            {
                _context.Entry(concurso).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaseContestExists(concurso.Id))//deixar esse método genérico em repositories
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(concurso);
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
