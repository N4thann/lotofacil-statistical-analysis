using System.Text;

namespace Lotofacil.Domain.Entities
{
    public class Contest : ContestBaseEntity
    {
        public Contest(string name, DateTime data, string numbers)
        {
            Name = name;
            Data = data;
            Numbers = numbers;
        }
        public DateTime? LastProcessed { get; set; }
    }
}
