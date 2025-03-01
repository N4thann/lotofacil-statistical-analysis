using DocumentFormat.OpenXml.Office.CustomUI;
using Lotofacil.Application.DTO.Request;
using Lotofacil.Application.DTO.Response;
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

        //public async Task<ContestModalResponseDTO> AnalisarConsursos(ContestModalRequestDTO request)
        //{
        //    var tasks = request.Contests.Select(id => _repository.GetByIdAsync(id)); // Retorna IEnumerable<Task<BaseContest>>
        //    var contests = await Task.WhenAll(tasks); // Aguarda todas as Tasks

        //    // Dicionário para contar as ocorrências dos números (1 a 25)
        //    var occurrences = new Dictionary<int, int>();
        //    for (int i = 1; i <= 25; i++)
        //    {
        //        occurrences[i] = 0;
        //    }

        //    foreach (var item in contests)
        //    {
        //        var listNumbers = _contestMS.ConvertFormattedStringToList(item.Numbers);

        //        foreach (var number in listNumbers)
        //        {
        //            if (occurrences.ContainsKey(number))
        //            {
        //                occurrences[number]++;
        //            }
        //        }

        //        // Pegando os 5 números mais frequentes com suas contagens
        //        var top5MostFrequentNumbers = occurrences
        //            .OrderByDescending(x => x.Value) // Maior frequência primeiro
        //            .ThenBy(x => x.Key) // Desempata pelo menor número
        //            .Take(5)
        //            .ToDictionary(x => x.Key, x => x.Value); // Mantém o formato Dictionary<int, int>

        //        // Pegando os 5 números menos frequentes com suas contagens
        //        var top5LeastFrequentNumbers = occurrences
        //            .OrderBy(x => x.Value) // Menor frequência primeiro
        //            .ThenBy(x => x.Key) // Desempata pelo menor número
        //            .Take(5)
        //            .ToDictionary(x => x.Key, x => x.Value); // Mantém o formato Dictionary<int, int>

        //        var top5MostFrequentNumbersList = top5MostFrequentNumbers.Keys.ToList();
        //        var top5LeastFrequentNumbersList = top5LeastFrequentNumbers.Keys.ToList();

        //    }
        //}
    }
}
