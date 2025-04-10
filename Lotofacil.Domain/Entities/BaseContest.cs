using System.Text;

namespace Lotofacil.Domain.Entities
{
    /// <summary>
    /// Representa um concurso base utilizado como referência para a comparação com outros concursos.
    /// A ideia é selecionar um concurso específico e utilizá-lo como base para compará-lo com todos os outros,
    /// exceto ele mesmo. Dessa forma, é possível analisar seu desempenho com base nos resultados comparativos.
    /// </summary>
    public class BaseContest : ContestBaseEntity
    {
        public BaseContest() { }    
        public BaseContest(string name, DateTime data, string numbers)
        {
            Name = name;
            Data = data;
            Numbers = numbers;
            ContestsAbove11 = new List<Contest>();
            CreatedAt = DateTime.Now;
            TopTenNumbers = string.Empty;
        }

        /// <summary>
        /// Obtém o número de concursos em que foram acertados exatamente 11 números.
        /// </summary>
        public int Hit11 { get; private set; }

        /// <summary>
        /// Obtém o número de concursos em que foram acertados exatamente 12 números.
        /// </summary>
        public int Hit12 { get; private set; }

        /// <summary>
        /// Obtém o número de concursos em que foram acertados exatamente 13 números.
        /// </summary>
        public int Hit13 { get; private set; }

        /// <summary>
        /// Obtém o número de concursos em que foram acertados exatamente 14 números.
        /// </summary>
        public int Hit14 { get; private set; }

        /// <summary>
        /// Obtém o número de concursos em que foram acertados exatamente 15 números.
        /// </summary>
        public int Hit15 { get; private set; }

        /// <summary>
        /// Obtém a data e hora em que o registro do concurso foi criado.
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Obtém ou define o total de concursos na lista ContestsAbove11 para controle no job do Hangfire "TopTenJobHandler"
        /// </summary>
        public int? TotalProcessed { get; set; }

        /// <summary>
        /// Obtém ou define os dez números mais frequentes nos concursos.
        /// </summary>
        public string TopTenNumbers { get; private set; }

        /// <summary>
        /// Obtém ou define a coleção de concursos em que foram acertados 11 ou mais números.
        /// </summary>
        public virtual ICollection<Contest> ContestsAbove11 { get; set; }

        /// <summary>
        /// Incrementa em 1 a contagem de concursos com 11 acertos.
        /// </summary>
        public void AddHit11()
        {
            Hit11 += 1;
        }

        /// <summary>
        /// Incrementa em 1 a contagem de concursos com 12 acertos.
        /// </summary>
        public void AddHit12()
        {
            Hit12 += 1;
        }

        /// <summary>
        /// Incrementa em 1 a contagem de concursos com 13 acertos.
        /// </summary>
        public void AddHit13()
        {
            Hit13 += 1;
        }

        /// <summary>
        /// Incrementa em 1 a contagem de concursos com 14 acertos.
        /// </summary>
        public void AddHit14()
        {
            Hit14 += 1;
        }

        /// <summary>
        /// Incrementa em 1 a contagem de concursos com 15 acertos.
        /// </summary>
        public void AddHit15()
        {
            Hit15 += 1;
        }

        /// <summary>
        /// Define os dez números mais frequentes do concurso.
        /// </summary>
        /// <param name="numbers">String contendo os números mais frequentes. Deve ter no máximo 30 caracteres.</param>
        /// <exception cref="ArgumentException">Lançado quando a string ultrapassa o limite de 30 caracteres.</exception>
        public void AddTopTenNumbers(string numbers)
        {
            if (numbers.Length > 30)
            {
                throw new ArgumentException("A string deve conter no máximo 30 caracteres.", nameof(numbers));
            }

            TopTenNumbers = numbers;
        }
    }
}
