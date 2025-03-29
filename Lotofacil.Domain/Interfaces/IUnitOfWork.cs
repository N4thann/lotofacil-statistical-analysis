namespace Lotofacil.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Esse código cria dinamicamente o repositório necessário (caso ainda não exista no dicionário _repositories) e o devolve. 
        /// Isso evita que você tenha que criar manualmente instâncias dos repositórios para cada entidade.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
        /// <summary>
        /// Batch de operações: Em vez de salvar mudanças após cada atualização dentro do loop, todas as operações são salvas de uma vez no final 
        /// usando await _unitOfWork.CompleteAsync(). Isso reduz as idas e voltas ao banco de dados.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> CompleteAsync(CancellationToken cancellationToken = default);
    }
}
