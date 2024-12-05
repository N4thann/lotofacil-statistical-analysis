using Lotofacil.Application.Services.Interfaces;
using Lotofacil.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lotofacil.Application.BackgroundJobs
{
    public class LogJobHandler : IJobHandler
    {
        private readonly IContestRepository _repositoryC;
        private readonly IContestManagementService _contestMS;
        private static readonly SemaphoreSlim _semaphore = new(1, 1);

        public async Task ExecuteAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                Console.WriteLine("Iniciando execução do LogJobHandler...");

                await SaveLogsAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro durante a execução do LogJobHandler: {ex.Message}");
            }
            finally
            {
                _semaphore.Release();
                Console.WriteLine("Recurso liberado.");
            }
        }

        private async Task SaveLogsAsync()
        {
            try
            {
                var listContest = await _repositoryC.GetAllWithBaseContestsOrderedAsync();

                if (listContest.Any())
                {
                    foreach (var y in listContest)
                    {
                        

                    }
                }
            }
            catch (Exception ex) 
            { 
                
            }
            finally
            {

            }
        }
    }
}
