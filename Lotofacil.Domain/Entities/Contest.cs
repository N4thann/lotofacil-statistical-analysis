using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lotofacil.Domain.Entities
{
    public class Contest
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("Name")]
        [MaxLength(20), MinLength(6)]
        public string Name { get; set; }

        [Required]
        [Column("Data")]
        public DateTime Data { get; set; }

        [Required]
        [MaxLength(45), MinLength(30)]
        [Column("Numbers")]
        public string Numbers { get; set; }
    }
}
