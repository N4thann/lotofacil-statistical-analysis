using Lotofacil.Application.Services.Interfaces;
using Lotofacil.Domain.Entities;
using Lotofacil.Domain.Interfaces;

namespace Lotofacil.Application.BackgroundJobs
{
    public class MainJobHandler : IJobHandler//Todos os BackgroundJobs devem implementar essa interface
    {
        private readonly IRepository<BaseContest> _repositoryBC;
        private readonly IRepository<Contest> _repositoryC;
        private readonly IContestManagementService _contestMS;
        private static readonly SemaphoreSlim _semaphore = new(1, 1);

        public MainJobHandler(
            IRepository<BaseContest> repositoryBC, 
            IRepository<Contest> repositoryC,  
            IContestManagementService contestMS)
        {
            _repositoryBC = repositoryBC;
            _repositoryC = repositoryC;
            _contestMS = contestMS;
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
            var baseContests = await _repositoryBC.GetAllAsync();

            var contests = await _repositoryC.GetAllAsync();

            var allHits = 0;

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

                            if (allHits > 10 && !x.ContestsAbove11.Contains(y))
                            {
                                x.ContestsAbove11.Add(y);
                                y.BaseContests.Add(x);
                                await _repositoryC.UpdateAsync(y);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Concurso {y.Name}: já está atualizado.");
                        }
                        await _repositoryBC.UpdateAsync(x);
                    }
                }

                foreach (var y in contests)
                {
                    y.LastProcessed = DateTime.Now;
                    await _repositoryC.UpdateAsync(y);
                }
                await UpdateHitsAsync();
            }
            else
            {
                Console.WriteLine("A lista Contests ou BaseContest está vazia.");
            }
        }

        private async Task UpdateHitsAsync()
        {
            var baseContests = await _repositoryBC.GetAllAsync();

            if (baseContests.Any())
            {
                foreach (var baseContest in baseContests)
                {
                    bool hasUpdates = false;

                    foreach (var contest in baseContest.ContestsAbove11)
                    {
                        var numbersBC = _contestMS.ConvertFormattedStringToList(baseContest.Numbers);
                        var numbersC = _contestMS.ConvertFormattedStringToList(contest.Numbers);

                        var allHits = _contestMS.CalculateIntersection(numbersBC, numbersC);

                        switch (allHits)
                        {
                            case 11: baseContest.AddHit11(); hasUpdates = true; break;
                            case 12: baseContest.AddHit12(); hasUpdates = true; break;
                            case 13: baseContest.AddHit13(); hasUpdates = true; break;
                            case 14: baseContest.AddHit14(); hasUpdates = true; break;
                            case 15: baseContest.AddHit15(); hasUpdates = true; break;
                        }

                    }

                    if (hasUpdates)
                    {
                        Console.WriteLine($"Atualizando BaseContest: {baseContest.Name}, Hits: 11={baseContest.Hit11}, 12={baseContest.Hit12}, 13={baseContest.Hit13}, 14={baseContest.Hit14}, 15={baseContest.Hit15}");
                        await _repositoryBC.UpdateAsync(baseContest);
                    }
                }

                Console.WriteLine("Incrementos atualizados com sucesso.");
            }
            else
            {
                Console.WriteLine("A lista BaseContest está vazia.");
            }

        }
    }
}