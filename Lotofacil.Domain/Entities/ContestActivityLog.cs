using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lotofacil.Domain.Entities
{
    public class ContestActivityLog : ContestBaseEntity
    {
        public bool MatchedAnyBaseContest { get; private set; }

        public string ?BaseContestName { get; private set; }

        public string ?BaseContestNumbers { get; private set; }

        public DateTime CreateTime { get; private set; }

        //Inicializa os atributos quando um Concurso não deu Matched com nenhum Concurso Base
        public void InitializeWithoutBaseContest(
            string name, 
            string numbers, 
            DateTime data)
        {
            SetCommonAttributes(name, numbers, data);
            MatchedAnyBaseContest = false;
            BaseContestName = null;
            BaseContestNumbers = null;
        }
        //Inicializa os atributos quando um Concurso deu Matched com um Concurso Base
        public void InitializeWithBaseContest(
            string name, 
            string numbers, 
            DateTime data, 
            string baseContestName, 
            string baseContestNumbers) 
        {
            SetCommonAttributes(name, numbers, data);
            MatchedAnyBaseContest = true;
            BaseContestName = baseContestName;
            BaseContestNumbers = baseContestNumbers;
        }
        //Seta em ambos os métodos acima os mesmos atributos
        private void SetCommonAttributes(string name, string numbers, DateTime data)
        {
            Name = name;
            Numbers = numbers;
            Data = data;
            CreateTime = DateTime.Now;
        }
    }
}
