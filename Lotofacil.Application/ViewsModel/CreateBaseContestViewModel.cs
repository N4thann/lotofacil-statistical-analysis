using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Lotofacil.Application.ViewsModel
{
    public class CreateBaseContestViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20), MinLength(2)]
        [Display(Name = "Descrição")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Data")]
        public DateTime Data { get; set; }

        [Required]
        [MaxLength(45), MinLength(30)]
        [Display(Name = "Numeros do Concurso Base")]
        public string BaseContestName { get; set; }
    }
}
