using Lotofacil.Application.Services.Interfaces;
using Lotofacil.Domain.Entities;
using Lotofacil.Domain.Interfaces;
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
            /*
            var baseContests = await _repositoryBC.GetAllAsync();

            var contests = await _repositoryC.GetAllAsync();

            foreach (var x in baseContests)
            {
                var numbersBC = _contestMS.ConvertFormattedStringToList(x.Numbers);

                foreach (var y in contests)
                {
                    var numbersC = _contestMS.ConvertFormattedStringToList(y.Numbers);

                    var allHits = _contestMS.CalculateIntersection(numbersBC, numbersC);

                    y.Processed = true;

                    var log = new ContestActivityLog();
                    if (allHits > 10)
                    {
                        
                        log.InitializeWithBaseContest(
                            x.Name,
                            x.Numbers,
                            x.Data,
                            y.Name,
                            y.Numbers);
                    }
                    else
                    {
                        log.InitializeWithoutBaseContest(
                            x.Name,
                            x.Numbers,
                            x.Data);
                    }
                    

                    if (allHits == 11)
                    {
                        x.AddMatched11();
                        x.ContestsAbove11.Add(y);
                    }
                    if (allHits == 12)
                    {
                        x.AddMatched12();
                        x.ContestsAbove11.Add(y);
                    }
                    if (allHits == 13)
                    {
                        x.AddMatched13();
                        x.ContestsAbove11.Add(y);
                    }
                    if (allHits == 14)
                    {
                        x.AddMatched14();
                        x.ContestsAbove11.Add(y);
                    }
                    if (allHits == 15)
                    {
                        x.AddMatched15();
                        x.ContestsAbove11.Add(y);
                    }

                }
            }
            */
        }
    }
}
