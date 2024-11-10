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
            Numbers = FormatNumbersToSave(numbers);
            ContestsAbove11 = new List<Contest>();
        }

        public int Matched11 { get; private set; }
        public int Matched12 { get; private set; }
        public int Matched13 { get; private set; }
        public int Matched14 { get; private set; }
        public int Matched15 { get; private set; }

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
        public string FormatNumbersToSave(string numbers)
        {
            // Converte a string em uma lista de substrings de dois caracteres
            var formattedNumbers = Enumerable
                .Range(0, numbers.Length / 2)
                .Select(i => numbers.Substring(i * 2, 2));

            // Junta as substrings com "-"
            return string.Join("-", formattedNumbers);
        }
    }
}
