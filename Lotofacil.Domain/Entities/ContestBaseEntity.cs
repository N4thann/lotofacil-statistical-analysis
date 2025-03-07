namespace Lotofacil.Domain.Entities
{
    /// <summary>
    /// Classe base para representar um concurso, fornecendo propriedades comuns compartilhadas por todas as entidades relacionadas.
    /// </summary>
    public abstract class ContestBaseEntity
    {
        /// <summary>
        /// Obtém ou define o identificador único do concurso.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtém ou define o nome do concurso.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Obtém ou define a data de realização do concurso.
        /// </summary>
        public DateTime Data { get; set; }

        /// <summary>
        /// Obtém ou define os números sorteados no concurso.
        /// </summary>
        public string Numbers { get; set; }
    }
}
