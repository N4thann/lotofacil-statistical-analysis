using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lotofacil.Domain.Entities
{
    /// <summary>
    /// Representa um registro de atividade de um concurso, armazenando informações 
    /// sobre a comparação entre um concurso diário e um concurso base somente se houver mais de 11 acertos.
    /// </summary>
    public class ContestActivityLog : ContestBaseEntity
    {
        /// <summary>
        /// Construtor padrão exigido pelo Entity Framework.
        /// </summary>
        public ContestActivityLog() { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="ContestActivityLog"/>, registrando a comparação 
        /// entre um concurso diário e um concurso base.
        /// </summary>
        /// <param name="name">O nome do concurso diário.</param>
        /// <param name="numbers">Os números sorteados no concurso diário.</param>
        /// <param name="data">A data de realização do concurso diário.</param>
        /// <param name="bcName">O nome do concurso base utilizado para comparação.</param>
        /// <param name="bcNumbers">Os números sorteados no concurso base.</param>
        /// <param name="countHits">A quantidade de números acertados na comparação.</param>
        public ContestActivityLog(
            string name,
            string numbers,
            DateTime data,
            string bcName,
            string bcNumbers,
            int countHits)
        {
            Name = name;
            Numbers = numbers;
            Data = data;
            BaseContestName = bcName;
            BaseContestNumbers = bcNumbers;
            CountHits = countHits;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// Obtém o nome do concurso base associado ao concurso diário.
        /// </summary>
        public string BaseContestName { get; private set; }

        /// <summary>
        /// Obtém os números sorteados no concurso base.
        /// </summary>
        public string BaseContestNumbers { get; private set; }

        /// <summary>
        /// Obtém a quantidade de números acertados na comparação entre o concurso diário e o concurso base.
        /// </summary>
        public int CountHits { get; private set; }

        /// <summary>
        /// Obtém a data e hora em que o registro da atividade foi criado.
        /// </summary>
        public DateTime CreateTime { get; private set; }
    }
}
