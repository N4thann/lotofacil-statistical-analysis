using Lotofacil.Application.Services;
using Lotofacil.Application.Services.Interfaces;
using Lotofacil.Domain.Interfaces;

namespace Lotofacil.Application.BackgroundJobs
{
    /// <summary>
    /// Handles the job responsible for analyzing the top 10 most frequent numbers
    /// in a base contest by iterating through its associated contests.
    /// The results are stored in the <see cref="BaseContest.TopTenNumbers"/> property,
    /// which can be used for real-time updates in dashboards.
    /// </summary>
    public class TopTenJobHandler
    {
        private readonly IBaseContestService _baseContestService;
        private readonly IContestManagementService _contestMS;
        private readonly IBaseContestRepository _repositoryBC;
        private static readonly SemaphoreSlim _semaphore = new(1, 1);

        /// <summary>
        /// Initializes a new instance of the <see cref="TopTenJobHandler"/> class with the required services 
        /// and repository for managing base contests and contest relationships.
        /// </summary>
        /// <param name="repositoryBC">Repository for managing base contests.</param>
        /// <param name="contestMS">Service for handling contest-related operations.</param>
        /// <param name="baseContestService">Service for retrieving base contests with associated contests.</param>
        public TopTenJobHandler(IBaseContestRepository repositoryBC,
            IContestManagementService contestMS,
            IBaseContestService baseContestService)
        {
            _repositoryBC = repositoryBC;
            _contestMS = contestMS;
            _baseContestService = baseContestService;
        }

        /// <summary>
        /// Executes the job to calculate the top 10 most frequent numbers for each base contest.
        /// The job navigates through the associated contests, counts occurrences of numbers,
        /// and updates the <see cref="BaseContest.TopTenNumbers"/> property.
        /// Uses a semaphore to ensure thread-safe execution.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task ExecuteAsync()
        {
           await _semaphore.WaitAsync();
            try
            {
                // Obter todos os concursos base
                var baseContests = await _baseContestService.GetAllWithContestsAbove11Async();

                foreach (var baseContest in baseContests)
                {
                    var occurrences = Enumerable.Range(1, 25).ToDictionary(i => i, _ => 0);//Substitui um for tradicional para preencher o valor do dictionary

                    // Contabilizar ocorrências dos números nos concursos
                    foreach (var subContest in baseContest.ContestsAbove11)
                    {
                        var numbers = _contestMS.ConvertFormattedStringToList(subContest.Numbers);
                        foreach (var number in numbers)
                        {
                            occurrences[number]++;
                        }
                    }

                    // Obter os 10 números mais frequentes, mantendo apenas as chaves (números)
                    var top10Numbers = occurrences
                        .OrderByDescending(x => x.Value) // Ordena pela frequência (maior primeiro)
                        .ThenBy(x => x.Key) // Em caso de empate, ordena pelo menor número
                        .Take(10)
                        .Select(x => x.Key) // Pega apenas os números
                        .OrderBy(x => x) // Ordena em ordem crescente
                        .Select(x => x.ToString("D2")) // Formata para "01", "02", etc.
                        .ToList();

                    //Chama um método da entidade para atribuir valor a variável TopTenNumbers
                    baseContest.AddTopTenNumbers(string.Join("-", top10Numbers));

                    // Salvar alterações no serviço
                    await _repositoryBC.UpdateBaseContestAsync(baseContest);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar o job: {ex.Message}");
            }
            finally
            {
                _semaphore.Release();
                Console.WriteLine("Recurso liberado.");
            }
        }
    }
}
