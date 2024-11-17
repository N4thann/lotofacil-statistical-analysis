using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lotofacil.Domain.Entities
{
    public class ContestActivityLog : ContestBaseEntity
    {
        public bool MatchedAnyBaseContest { get; set; }

        public string ?BaseContestName { get; set; }

        public string ?BaseContestNumbers { get; set; }

        public DateTime CreateTime { get; set; }

    }
}
