namespace Lotofacil.Domain.Enums
{
    /// <summary>
    /// Representa um Enum que é utilizado para tratar exceções globais informando um tipo de erro e uma mensagem para uma view adaptada
    /// de erro
    /// </summary>
    public enum ErrorType
    {
        Critical, // Erro crítico, geralmente falha de sistema
        NoRecords, // Nenhum registro encontrado
        NotFound, // Recurso não encontrado
        Unspecified
    }
}
