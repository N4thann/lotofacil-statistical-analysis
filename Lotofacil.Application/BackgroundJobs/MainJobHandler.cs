using Lotofacil.Application.Services.Interfaces;
using Lotofacil.Domain.Entities;
using Lotofacil.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication;

namespace Lotofacil.Application.BackgroundJobs
{
    public class MainJobHandler : IJobHandler//Todos os BackgroundJobs devem implementar essa interface
    {
        private readonly IBaseContestRepository _repositoryBC;
        private readonly IContestRepository _repositoryC;
        private readonly IRepository<ContestActivityLog> _repositoryLog;
        private readonly IContestManagementService _contestMS;
        private static readonly SemaphoreSlim _semaphore = new(1, 1);

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