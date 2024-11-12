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
            // Converte a string em uma lista de inteiros
            var numberList = new List<int>();
            for (int i = 0; i < numbers.Length; i += 2)
            {
                // Pega cada par de caracteres e converte para inteiro
                var num = int.Parse(numbers.Substring(i, 2));
                numberList.Add(num);
            }

            // Converte a lista de inteiros para o formato de string desejado
            return string.Join("-", numberList.Select(n => n.ToString("D2")));
        }
    }
}
