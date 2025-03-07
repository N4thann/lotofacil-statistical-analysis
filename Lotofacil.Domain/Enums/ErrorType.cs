namespace Lotofacil.Domain.Enums
{
    /// <summary>
    /// Define os tipos de erro utilizados no tratamento global de exceções,  
    /// permitindo a exibição de mensagens personalizadas em uma view de erro adaptada.
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        /// Erro crítico, geralmente causado por falhas no sistema.
        /// </summary>
        Critical,

        /// <summary>
        /// Nenhum registro foi encontrado na consulta.
        /// </summary>
        NoRecords,

        /// <summary>
        /// O recurso solicitado não foi localizado.
        /// </summary>
        NotFound,

        /// <summary>
        /// Tipo de erro não especificado.
        /// </summary>
        Unspecified
    }
}

