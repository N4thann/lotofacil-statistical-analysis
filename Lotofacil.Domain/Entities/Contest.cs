namespace Lotofacil.Domain.Entities
{
    /// <summary>
    /// Representa um concurso realizado, contendo seus números sorteados e suas associações 
    /// com concursos base para análise comparativa.
    /// </summary>
    public class Contest : ContestBaseEntity
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Contest"/> com o nome, data e números sorteados especificados.
        /// </summary>
        /// <param name="name">O nome do concurso.</param>
        /// <param name="data">A data de realização do concurso.</param>
        /// <param name="numbers">Os números sorteados no concurso.</param>
        public Contest(string name, DateTime data, string numbers)
        {
            Name = name;
            Data = data;
            Numbers = numbers;
            BaseContests = new List<BaseContest>();
        }

        /// <summary>
        /// Obtém ou define a data da última vez que o concurso foi processado pelo o job do Hangfire "MainJobHandler".
        /// </summary>
        public DateTime? LastProcessed { get; set; }

        /// <summary>
        /// Obtém ou define a coleção de concursos base nos quais este concurso teve 11 ou mais acertos.
        /// </summary>
        public virtual ICollection<BaseContest> BaseContests { get; set; }
    }
}
