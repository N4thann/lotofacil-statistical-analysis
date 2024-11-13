using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lotofacil.Domain.Entities
{
    public class ContestActivityLog
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Data { get; set; }

        public string Numbers { get; set; }

        public bool MatchedAnyBaseContest { get; set; }

        public string ?BaseContestName { get; set; }

        public string ?BaseContestNumbers { get; set; }

        public DateTime CreateTime { get; set; }

    }
}
