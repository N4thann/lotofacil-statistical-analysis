﻿using Lotofacil.Application.Services.Interfaces;
using Lotofacil.Domain.Entities;
using Lotofacil.Domain.Interfaces;

namespace Lotofacil.Application.BackgroundJobs
{
    public class MainJobHandler : IJobHandler//Todos os BackgroundJobs devem implementar essa interface
    {
        private readonly IRepository<BaseContest> _repositoryBC;
        private readonly IRepository<Contest> _repositoryC;
        private readonly IContestManagementService _contestMS;

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
            var baseContests = await _repositoryBC.GetAllAsync();

            var contests = await _repositoryC.GetAllAsync(); 

            if(baseContests.Any() && contests.Any())
            {
                foreach (var x in baseContests)
                {
                    var numbersBC = _contestMS.ConvertFormattedStringToList(x.Numbers);

                    foreach (var y in contests)
                    {
                        var numbersC = _contestMS.ConvertFormattedStringToList(y.Numbers);

                        if (y.LastProcessed == null || y.LastProcessed < x.CreatedAt)
                        {

                            var allHits = _contestMS.CalculateIntersection(numbersBC, numbersC);

                            switch (allHits)
                            {
                                case 11: x.AddMatched11(); break;
                                case 12: x.AddMatched12(); break;
                                case 13: x.AddMatched13(); break;
                                case 14: x.AddMatched14(); break;
                                case 15: x.AddMatched15(); break;
                            }

                            if(allHits > 10) x.ContestsAbove11.Add(y);
                        }
                        else
                        {
                            Console.WriteLine($"Concurso {y.Name}: já está atualizado.");
                        }
                    }

                    await _repositoryBC.UpdateAsync(x);
                }

                foreach (var y in contests)
                {
                    y.LastProcessed = DateTime.Now;
                    await _repositoryC.UpdateAsync(y);
                }
            }
            else
            {
                Console.WriteLine("A lista Contests ou BaseContest está vazia.");
            }           
        }
     }
}