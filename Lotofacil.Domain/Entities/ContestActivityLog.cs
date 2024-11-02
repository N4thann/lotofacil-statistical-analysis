using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lotofacil.Domain.Entities
{
    public class ContestActivityLog
    {
        [Key]
        [Column("Id")]
        public int Id{ get; set; }

        [Required]
        [Column("ContestName")]
        [MaxLength(20), MinLength(6)]
        public string ContestName { get; set; }

        [Required]
        [Column("ContestData")]
        public DateTime ContestData { get; set; }

        [Required]
        [MaxLength(45), MinLength(30)]
        [Column("ContestNumbers")]
        public string ContestNumbers { get; set; }

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
