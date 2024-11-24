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
            Processed = false;//No cadastro do Concurso não foi processado, apenas no serviço principal
        }
        public bool Processed { get; private set; }
    }
}
