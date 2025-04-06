using Lotofacil.Domain.Entities;
using Lotofacil.Infra.Data.Context;
using Lotofacil.Infra.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lotofacil.Infra.Data.Initialization
{
    public class ApplicationDbInitializer : IDataInitializer
    {
        public void Seed(ApplicationDbContext context)
        {
            context.Database.Migrate();

            if (!context.BaseContests.Any() && !context.Contests.Any())
            {
                var baseContests = new List<BaseContest>
                {
                    new BaseContest
                    {
                        Name = "Concurso 444",
                        Data = new DateTime(2009, 07, 16),
                        Numbers = "01-02-03-06-07-10-14-15-18-19-20-21-22-24-25"
                    },
                    new BaseContest
                    {
                        Name = "Concurso 888",
                        Data = new DateTime(2013, 04, 03),
                        Numbers = "01-03-06-11-12-13-14-16-17-18-19-20-21-22-24"
                    }
                };
           
                var contests = new List<Contest>
                {
                    new Contest
                    {
                        Name = "Concurso 1",
                        Data = new DateTime(2003, 09, 29),
                        Numbers = "02-03-05-06-09-10-11-13-14-16-18-20-23-24-25"
                    },
                    new Contest
                    {
                        Name = "Concurso 2",
                        Data = new DateTime(2003, 10, 06),
                        Numbers = "01-04-05-06-07-09-11-12-13-15-16-19-20-23-24"
                    }
                };

                context.BaseContests.AddRange(baseContests);
                context.Contests.AddRange(contests);

                context.SaveChanges();
            }
        }
    }
}
