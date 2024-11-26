using System.Text;

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
            CreatedAt = DateTime.Now;
        }

        public int Matched11 { get; private set; }
        public int Matched12 { get; private set; }
        public int Matched13 { get; private set; }
        public int Matched14 { get; private set; }
        public int Matched15 { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public virtual ICollection<Contest> ContestsAbove11 { get; set; }

        public void AddMatched11()
        {
            Matched11 = +1;
        }
        public void AddMatched12()
        {
            Matched12 = +1;
        }

        public void AddMatched13()
        {
            Matched13 = +1;
        }
        public void AddMatched14()
        {
            Matched14 = +1;
        }
        public void AddMatched15()
        {
            Matched15 = +1;
        }
    }
}
