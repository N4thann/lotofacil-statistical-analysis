using Lotofacil.Infra.Data.Context;
using Lotofacil.Domain.Entities;
using Lotofacil.Application.ViewsModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Lotofacil.Controllers
{
    public class ContestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContestController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult List()
        {
            try
            {
                var contest = _context.Contests
                .OrderByDescending(c => c.Data)
                .AsNoTracking()
                .ToList();

                return View(contest);
            }
            catch (Exception ex) 
            {
                return View("Error", new ErrorViewModel
                {
                    Message = "Erro ao acessar os dados do banco de dados da tabela Contests.",
                    ExceptionDetails = ex.Message
                });
            }            
        }

    }
}
