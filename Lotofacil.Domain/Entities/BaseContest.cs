using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lotofacil.Domain.Entities
{
    public class BaseContest
    {
        public BaseContest(int id, string name, DateTime data, string numbers)
        {
            Id = id;
            Name = name;
            Data = data;
            Numbers = numbers;
            ContestsAbove11 = new List<Contest>();
        }

        [Key]
        [Column("Id")]
        public int Id { get; private set; }

        [Required]
        [Column("Name")]
        [MaxLength(20), MinLength(6)]
        public string Name { get; private set; }

        [Required]
        [Column("Data")]
        public DateTime Data { get; set; }
        public int Matched11 { get; set; }
        public int Matched12 { get; set; }
        public int Matched13 { get; set; }
        public int Matched14 { get; set; }
        public int Matched15 { get; set; }

        [Required]
        [MaxLength(45), MinLength(30)]
        [Column("Numbers")]
        public string Numbers { get; private set; }

        public virtual ICollection<Contest> ContestsAbove11 { get; set; }
    }
}
