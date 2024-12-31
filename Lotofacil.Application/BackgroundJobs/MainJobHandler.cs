using Lotofacil.Application.Services;
using Lotofacil.Application.Services.Interfaces;
using Lotofacil.Domain.Entities;
using Lotofacil.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication;

namespace Lotofacil.Application.BackgroundJobs
{
    /// <summary>
    /// Handles the primary job in the system, which establishes relationships and performs comparisons 
    /// between daily contests and base contests that have been saved over time.
    /// The process calculates the intersection of contest numbers, which are sets of 15 numbers, 
    /// and logs the results when intersections meet specific criteria.
    /// </summary>
    public class MainJobHandler
    {
        private readonly IBaseContestRepository _repositoryBC;
        private readonly IContestRepository _repositoryC;
        private readonly IRepository<ContestActivityLog> _repositoryLog;
        private readonly IContestManagementService _contestMS;
        private static readonly SemaphoreSlim _semaphore = new(1, 1);

        /// <summary>
        /// Initializes a new instance of the <see cref="MainJobHandler"/> class with the required repositories
        /// and contest management service for handling contests and base contest relationships.
        /// </summary>
        /// <param name="repositoryBC">Repository for base contests.</param>
        /// <param name="repositoryC">Repository for daily contests.</param>
        /// <param name="repositoryLog">Repository for logging contest activities.</param>
        /// <param name="contestMS">Service for managing contests, including operations like number intersection.</param>
        public MainJobHandler(
            IBaseContestRepository repositoryBC, 
            IContestRepository repositoryC,  
            IContestManagementService contestMS,
            IRepository<ContestActivityLog> repositoryLog)
        {
            _repositoryBC = repositoryBC;
            _repositoryC = repositoryC;
            _contestMS = contestMS;
            _repositoryLog = repositoryLog;
        }

        /// <summary>
        /// Executes the primary job logic to compare daily contests with base contests,
        /// establish relationships, and log contest activities.
        /// Uses a semaphore to ensure thread-safe execution.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task ExecuteAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                Console.WriteLine("Iniciando execução do MainJobHandler...");

                await SaveRelationshipsAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro durante a execução do MainJobHandler: {ex.Message}");
            }
            finally
            {
                _semaphore.Release();
                Console.WriteLine("Recurso liberado.");
            }
        }

        /// <summary>
        /// Compares contests with base contests, calculates intersections of contest numbers, 
        /// and updates relationships and activity logs.
        /// Saves relationships where intersections exceed 10 and logs the activity 
        /// with detailed information about the match.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private async Task SaveRelationshipsAsync()
        {
            //Foi preciso criar repositórios específicos para recuperar as listas de ambas as entidades
            //O método do repositório genérico não tinha o include das listas e tinha o asnotrackin
            //Assim a abordagem do método do repositório genérico fica apenas listar as entidades
            //Já os específicos temos o update e o GetAll adaptados
            var baseContests = await _repositoryBC.GetAllWithContestsAbove11Async();

            var contests = await _repositoryC.GetAllWithBaseContestsAsync();

            int allHits = 0;

            if (baseContests.Any() && contests.Any())
            {
                foreach (var x in baseContests)
                {
                    var numbersBC = _contestMS.ConvertFormattedStringToList(x.Numbers);

                    foreach (var y in contests)
                    {
                        var numbersC = _contestMS.ConvertFormattedStringToList(y.Numbers);

                        allHits = 0;

                        if (y.LastProcessed == null || y.LastProcessed < x.CreatedAt)
                        {
                            allHits = _contestMS.CalculateIntersection(numbersBC, numbersC);

                            switch (allHits)
                            {
                                case 11: x.AddHit11(); break;
                                case 12: x.AddHit12(); break;
                                case 13: x.AddHit13(); break;
                                case 14: x.AddHit14(); break;
                                case 15: x.AddHit15(); break;
                            }

                            if (allHits > 10 && !x.ContestsAbove11.Contains(y))
                            {
                                var log = new ContestActivityLog(
                                    y.Name,
                                    y.Numbers,
                                    y.Data,
                                    x.Name,
                                    x.Numbers);

                                await _repositoryLog.AddAsync(log);

                                x.ContestsAbove11.Add(y);
                                y.BaseContests.Add(x);
                                await _repositoryC.UpdateContestAsync(y);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Concurso {y.Name}: já está atualizado.");
                        }
                    }
                    await _repositoryBC.UpdateBaseContestAsync(x);
                }

                foreach (var y in contests)
                {
                    y.LastProcessed = DateTime.Now;
                    await _repositoryC.UpdateContestAsync(y);
                }               
            }
            else
            {
                Console.WriteLine("A lista Contests ou BaseContest está vazia.");
            }
        }
    }
}