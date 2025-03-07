using Lotofacil.Domain.Entities;

namespace Lotofacil.Domain.Interfaces
{
    public interface IBaseContestRepository
    {
        /// <summary>
        /// Retorna uma lista de concursos base, incluindo suas listas de concursos associados, 
        /// ordenados pela data de criação. Utiliza <see cref="AsSplitQuery"/> para evitar explosão cartesiana 
        /// e melhorar a performance das consultas.
        /// </summary>
        /// <returns>Uma coleção de concursos base com suas respectivas listas de concursos.</returns>
        Task<IEnumerable<BaseContest>> GetAllWithContestsAbove11Async();

        /// <summary>
        /// Atualiza os dados de um concurso base existente.
        /// </summary>
        /// <param name="baseContest">Objeto representando o concurso base a ser atualizado.</param>
        Task UpdateBaseContestAsync(BaseContest baseContest);

        /// <summary>
        /// Obtém um concurso base pelo seu identificador.
        /// Utiliza o conceito de Explicit Loading para carregar os dados sob demanda, evitando consultas automáticas desnecessárias.
        /// </summary>
        /// <param name="id">Identificador único do concurso base.</param>
        /// <returns>O objeto <see cref="BaseContest"/> correspondente ao ID informado.</returns>
        Task<BaseContest> GetByIdAsync(int id);
    }
}

/*
📌 Diretrizes para escolha do carregamento de dados:
✔ Se deseja simplicidade e não se preocupa com consultas extras → Ative o Lazy Loading.
✔ Se quer mais controle e evitar consultas desnecessárias → Use Include() ou LoadAsync() manualmente.
✔ Se enfrenta problemas de desempenho devido a muitas consultas pequenas → Desative Lazy Loading e utilize Include() sempre que necessário.

🔹 Minha recomendação: 
Se estiver começando, evite ativar o Lazy Loading imediatamente. Prefira utilizar Include() e ajuste conforme a necessidade.
*/
