using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lotofacil.Domain.Entities
{
    public class ContestActivityLog : ContestEntity
    {
        [Required]
        [Column("MatchedAnyBaseContest")]
        public bool MatchedAnyBaseContest { get; set; }

        [Column("BaseContestName")]
        [MaxLength(20), MinLength(6)]
        public string ?BaseContestName { get; set; }

        [MaxLength(45), MinLength(30)]
        [Column("ContestNumbers")]
        public string ?BaseContestNumbers { get; set; }

        [Required]
        [Column("CreateTime")]
        public DateTime CreateTime { get; set; }

    }
}
