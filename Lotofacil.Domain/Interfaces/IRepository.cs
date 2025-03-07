﻿using System.Linq.Expressions;

namespace Lotofacil.Domain.Interfaces
{
    /// <summary>
    /// Interface genérica para repositórios, fornecendo operações básicas de CRUD e consultas.
    /// </summary>
    /// <typeparam name="T">Tipo da entidade.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Obtém uma entidade pelo ID.
        /// </summary>
        /// <param name="id">Identificador da entidade.</param>
        /// <returns>Entidade correspondente ao ID informado.</returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Obtém todas as entidades do repositório.
        /// </summary>
        /// <returns>Lista de entidades.</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Adiciona uma nova entidade ao repositório.
        /// </summary>
        /// <param name="entity">Entidade a ser adicionada.</param>
        Task AddAsync(T entity);

        /// <summary>
        /// Atualiza uma entidade existente no repositório.
        /// </summary>
        /// <param name="entity">Entidade a ser atualizada.</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Remove uma entidade do repositório com base no ID informado.
        /// </summary>
        /// <param name="id">Identificador da entidade a ser removida.</param>
        Task DeleteAsync(int id);

        /// <summary>
        /// Obtém um IQueryable para realizar consultas avançadas sobre as entidades.
        /// </summary>
        /// <returns>Consulta IQueryable.</returns>
        IQueryable<T> GetAllQueryable();

        /// <summary>
        /// Verifica se existe alguma entidade que atende ao critério informado.
        /// </summary>
        /// <param name="predicate">Expressão para filtrar a existência da entidade.</param>
        /// <returns>Verdadeiro se a entidade existir, falso caso contrário.</returns>
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    }
}
// Mesmo que esse método GetAllAsync() seja para uma listagem em uma tabela em que cada registro tenha uma opção
// de edição, o AsNoTracking ainda irá otimizar apenas para o carregamento da tabela 
// e posteriormente posso fazer uma procura sem o AsNoTracking para procurar o registro com GetByIdAsync       

/* Por que GetAllQueryable não precisa ser assíncrono?
IQueryable representa apenas a definição da consulta e não a execução dela.
Ele não faz nenhuma operação no banco até que seja "materializado" (com ToList, First, etc.).
Tornar o método assíncrono criaria complexidade desnecessária sem ganho real.
O compilador não permite o uso de await para um retorno que já é materializado de forma síncrona.
 */
