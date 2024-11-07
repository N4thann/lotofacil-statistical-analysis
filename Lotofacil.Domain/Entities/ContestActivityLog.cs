using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lotofacil.Domain.Entities
{
    public class ContestActivityLog : ContestEntity
    {
        public bool MatchedAnyBaseContest { get; set; }

        public string ?BaseContestName { get; set; }

        public string ?BaseContestNumbers { get; set; }

        public DateTime CreateTime { get; set; }

    }
}
