using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lotofacil.Domain.Entities
{
    public class BaseContest : ContestBaseEntity
    {
        public BaseContest(string name, DateTime data, string numbers)
        {
            Name = name;
            Data = data;
            Numbers = numbers;
            ContestsAbove11 = new List<Contest>();
        }

        public int Matched11 { get; set; }
        public int Matched12 { get; set; }
        public int Matched13 { get; set; }
        public int Matched14 { get; set; }
        public int Matched15 { get; set; }

        public virtual ICollection<Contest> ContestsAbove11 { get; set; }

        public void AddMatched(int matched)
        {
            matched = +1;
        }

    }
}
