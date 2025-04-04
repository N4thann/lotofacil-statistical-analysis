using DocumentFormat.OpenXml.Office.CustomUI;
using Lotofacil.Application.DTO.Request;
using Lotofacil.Application.DTO.Response;
using Lotofacil.Application.Services.Interfaces;
using Lotofacil.Application.ViewsModel;
using Lotofacil.Domain.Entities;
using Lotofacil.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;

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
            Log.Debug("Obtendo lista de concursos com ordenação: {SortOrder}", sortOrder);

            var contests = await _repository.GetAllAsync();

            if (contests == null || !contests.Any())
            {
                Log.Warning("Nenhum concurso encontrado no banco de dados.");
                return Enumerable.Empty<Contest>();//Retorna Enumerable.Empty<Contest>() se não houver dados, em vez de null(Evita exceções).
            }

            var orderedContests = sortOrder switch
            {
                "DateAsc" => contests.OrderBy(c => c.Data),
                "DateDesc" => contests.OrderByDescending(c => c.Data),
                _ => contests.OrderByDescending(c => c.Data),
            };

            Log.Information("Retornando {TotalContests} concursos ordenados por {SortOrder}.", contests.Count(), sortOrder);

            return orderedContests;
        }

        public async Task CreateAsync(ContestViewModel contestVM)
        {
            var contestLog = Log.ForContext("ConcursoBaseId", contestVM.Name);

            contestLog.Debug("Iniciando criação de concurso");

            var formattedName = $"Concurso {contestVM.Name}";

            var contest = new Contest(
                formattedName,
                _contestMS.SetDataHour(contestVM.Data),
                _contestMS.FormatNumbersToSave(contestVM.Numbers)
            );

            await _repository.SaveAddAsync(contest);

            contestLog.Information("Concurso {ContestName} criado com sucesso.", formattedName);
        }

        public async Task<ContestModalResponseDTO> AnalisarConcursos(ContestModalRequestDTO request)
        {
            Log.Debug("Analisando concursos com os seguintes IDs: {ContestIds}", string.Join(", ", request.Contests));

            var contests = new List<Contest>();

            foreach (var id in request.Contests)
            {
                var contest = await _repository.GetByIdAsync(id);
                if (contest != null)
                {
                    contests.Add(contest);
                }
                else
                {
                    Log.Warning("Concurso com ID {Id} não encontrado.", id);
                }
            }

            if (!contests.Any())
            {
                Log.Warning("Nenhum concurso válido foi encontrado para análise.");
                return new ContestModalResponseDTO(new List<string>(), 0, 0, new List<int>(), new List<int>(), 0);
            }

            List<string> contestsName = contests.Select(c => c.Name).ToList();

            var occurrences = Enumerable.Range(1, 25).ToDictionary(i => i, _ => 0);
            int totalNumbers = 0;
            int evenCount = 0;
            int oddCount = 0;
            int multiplesOfThreeCount = 0;

            foreach (var contest in contests)
            {
                var listNumbers = _contestMS.ConvertFormattedStringToList(contest.Numbers);
                totalNumbers += listNumbers.Count;

                foreach (var number in listNumbers)
                {
                    if (occurrences.ContainsKey(number))
                        occurrences[number]++;

                    if (number % 2 == 0) evenCount++;
                    else oddCount++;

                    if (number % 3 == 0) multiplesOfThreeCount++;
                }
            }

            var top5MostFrequentNumbers = occurrences
                .OrderByDescending(x => x.Value)
                .ThenBy(x => x.Key)
                .Take(5)
                .Select(x => x.Key)
                .ToList();

            var top5LeastFrequentNumbers = occurrences
                .OrderBy(x => x.Value)
                .ThenBy(x => x.Key)
                .Take(5)
                .Select(x => x.Key)
                .ToList();

            double evenNumbersAveragePercentage = (totalNumbers > 0) ? (evenCount / (double)totalNumbers) * 100 : 0;
            double oddNumbersAveragePercentage = (totalNumbers > 0) ? (oddCount / (double)totalNumbers) * 100 : 0;
            double multiplesOfThreeAveragePercentage = (totalNumbers > 0) ? (multiplesOfThreeCount / (double)totalNumbers) * 100 : 0;

            Log.Information("Análise de concursos concluída. {TotalContests} concursos processados.", contests.Count);

            return new ContestModalResponseDTO(
                contestsName,
                evenNumbersAveragePercentage,
                oddNumbersAveragePercentage,
                top5MostFrequentNumbers,
                top5LeastFrequentNumbers,
                multiplesOfThreeAveragePercentage
            );
        }

    }
}
