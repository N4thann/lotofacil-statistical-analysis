using Lotofacil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotofacil.Domain.Interfaces
{
    public interface IContestRepository
    {
        /// <summary>
        /// Retorna uma lista de concursos, incluindo suas listas de concursos base associados, 
        /// ordenados pela data de criação. Utiliza <see cref="AsSplitQuery"/> para evitar explosão cartesiana 
        /// e melhorar a performance das consultas.
        /// </summary>
        /// <returns>Uma coleção de concursos com suas respectivas listas de concursos base.</returns>
        Task<IEnumerable<Contest>> GetAllWithBaseContestsAsync();
        /// <summary>
        /// Atualiza os dados de um concurso existente.
        /// </summary>
        /// <param name="baseContest">Objeto representando o concurso a ser atualizado.</param>
        Task UpdateContestAsync(Contest contest);
    }
}
