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

        public async Task<ContestModalResponseDTO> AnalisarConcursos(ContestModalRequestDTO request)
        {
            // 1. Buscar concursos no banco de dados
            var contests = new List<Contest>();
            foreach (var id in request.Contests)
            {
                var contest = await _repository.GetByIdAsync(id);
                if (contest != null)
                {
                    contests.Add(contest);
                }
            }
            // 2. Inicializar contadores e dicionário de ocorrências
            var occurrences = Enumerable.Range(1, 25).ToDictionary(i => i, _ => 0);

            int totalNumbers = 0;
            int evenCount = 0;
            int oddCount = 0;
            int multiplesOfThreeCount = 0;

            // 3. Processar cada concurso e extrair estatísticas
            foreach (var contest in contests)
            {
                var listNumbers = _contestMS.ConvertFormattedStringToList(contest.Numbers);
                totalNumbers += listNumbers.Count; // Adiciona a quantidade de números analisados

                foreach (var number in listNumbers)
                {
                    if (occurrences.ContainsKey(number))
                        occurrences[number]++; // Conta a frequência do número

                    if (number % 2 == 0) evenCount++; // Conta números pares
                    else oddCount++; // Conta números ímpares

                    if (number % 3 == 0) multiplesOfThreeCount++; // Conta múltiplos de 3
                }
            }

            // 4. Obter os 5 números mais e menos frequentes
            var top5MostFrequentNumbers = occurrences
                .OrderByDescending(x => x.Value) // Maior frequência primeiro
                .ThenBy(x => x.Key) // Desempate pelo menor número
                .Take(5)
                .Select(x => x.Key)
                .ToList();

            var top5LeastFrequentNumbers = occurrences
                .OrderBy(x => x.Value) // Menor frequência primeiro
                .ThenBy(x => x.Key) // Desempate pelo menor número
                .Take(5)
                .Select(x => x.Key)
                .ToList();

            // 5. Calcular as porcentagens médias
            double evenNumbersAveragePercentage = (totalNumbers > 0) ? (evenCount / (double)totalNumbers) * 100 : 0;
            double oddNumbersAveragePercentage = (totalNumbers > 0) ? (oddCount / (double)totalNumbers) * 100 : 0;
            double multiplesOfThreeAveragePercentage = (totalNumbers > 0) ? (multiplesOfThreeCount / (double)totalNumbers) * 100 : 0;

            // 6. Retornar os resultados
            return new ContestModalResponseDTO(
                evenNumbersAveragePercentage,
                oddNumbersAveragePercentage,
                top5MostFrequentNumbers,
                top5LeastFrequentNumbers,
                multiplesOfThreeAveragePercentage
            );
        }

    }
}
