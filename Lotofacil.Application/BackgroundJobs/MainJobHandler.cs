using Lotofacil.Application.Services.Interfaces;
using Lotofacil.Domain.Entities;
using Lotofacil.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotofacil.Application.BackgroundJobs
{
    public class MainJobHandler : IJobHandler//Todos os BackgroundJobs devem implementar essa interface
    {
        private readonly IRepository<BaseContest> _repositoryBC;
        private readonly IRepository<Contest> _repositoryC;
        private readonly IRepository<ContestActivityLog> _repositoryLog;
        private readonly IContestManagementService _contestMS;

        public MainJobHandler(
            IRepository<BaseContest> repositoryBC, 
            IRepository<Contest> repositoryC, 
            IRepository<ContestActivityLog> repositoryLog, 
            IContestManagementService contestMS)
        {
            _repositoryBC = repositoryBC;
            _repositoryC = repositoryC;
            _repositoryLog = repositoryLog;
            _contestMS = contestMS;
        }

        public async Task ExecuteAsync()
        {
            var baseContests = await _repositoryBC.GetAllAsync();

            var contests = await _repositoryC.GetAllAsync();

            foreach (var x in baseContests)
            {
                var numbersBC = _contestMS.ConvertFormattedStringToList(x.Numbers);

                foreach (var y in contests)
                {

                    var numbersC = _contestMS.ConvertFormattedStringToList(y.Numbers);

                    var allHits = _contestMS.CalculateIntersection(numbersBC, numbersC);


                    var log = new ContestActivityLog();

                    if (allHits > 10)
                    {
                        
                        log.InitializeWithBaseContest(
                            x.Name,
                            x.Numbers,
                            x.Data,
                            y.Name,
                            y.Numbers);

                        await _repositoryLog.AddAsync(log);
                    }
                    else
                    {
                        log.InitializeWithoutBaseContest(
                            x.Name,
                            x.Numbers,
                            x.Data);

                        await _repositoryLog.AddAsync(log);
                    }

                    switch (allHits)
                    {
                        case 11: x.AddMatched11(); break;
                        case 12: x.AddMatched12(); break;
                        case 13: x.AddMatched13(); break;
                        case 14: x.AddMatched14(); break;
                        case 15: x.AddMatched15(); break;
                    }

                    x.ContestsAbove11.Add(y);
                }

                await _repositoryBC.UpdateAsync(x);
            }

            foreach (var contest in contests)
            {
                await _repositoryC.UpdateAsync(contest);
            }
        }
    }
}
//if (contest.LastProcessed == null || contest.LastProcessed < latestBaseContest.CreatedAt)