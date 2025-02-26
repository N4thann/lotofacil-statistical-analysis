using Lotofacil.Application.DTO.Request;
using Lotofacil.Application.Services.Interfaces;
using Lotofacil.Application.ViewsModel;
using Lotofacil.Domain.Entities;
using Lotofacil.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Lotofacil.Application.Services
{
    public class ContestService : IContestService
    {
        private readonly IRepository<Contest> _repository;
        private readonly IContestManagementService _contestMS;
        public ContestService(IRepository<Contest> repository, IContestManagementService contestMS)
        {
            _repository = repository;
            _contestMS = contestMS;
        }

        public async Task<IEnumerable<Contest>> GetContestsOrderedAsync(string sortOrder)
        {

            var contests = await _repository.GetAllAsync();

            return sortOrder switch
            {
                "DateAsc" => contests.OrderBy(c => c.Data),
                "DateDesc" => contests.OrderByDescending(c => c.Data),
                _ => contests.OrderByDescending(_ => _.Data),
            };
        }

        public async Task CreateAsync(ContestViewModel contestVM)
        {
            var formattedName = $"Concurso {contestVM.Name}";

            //Deve ser atribuído assim, visto que o objeto só pode ser criado passando os parâmetros para o construtor
            var contest = new Contest(formattedName, _contestMS.SetDataHour(contestVM.Data), _contestMS.FormatNumbersToSave(contestVM.Numbers));

            await _repository.AddAsync(contest);//O método AddAsync do repositório já é assíncrono, então precisamos await essa chamada.
        }

        [Route("List/AnalisarConcursos")]
        public async Task<JsonResult> AnalisarConcursos([FromBody] ContestModalRequestDTO request)
        {
            if(request.Contests.Any())
                return Json(new { sucesso = false, mensagem = "É necessário bla bla" });
            else
                return Json(new { sucesso = true, mensagem = "errados" });
        }
    }
}
