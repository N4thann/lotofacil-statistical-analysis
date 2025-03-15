using Lotofacil.Application.Services;
using Lotofacil.Application.Services.Interfaces;
using Lotofacil.Domain.Entities;
using Lotofacil.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Serilog;

namespace Lotofacil.Application.BackgroundJobs
{
    /// <summary>
    /// Responsável pelo job principal do sistema, que estabelece relações e realiza comparações 
    /// entre os concursos diários e os concursos base armazenados ao longo do tempo.
    /// O processo calcula a interseção dos números dos concursos, que são conjuntos de 15 números,
    /// e registra os resultados quando determinadas condições são atendidas.
    /// </summary>
    public class MainJobHandler
    {
        private readonly IBaseContestRepository _repositoryBC;
        private readonly IContestRepository _repositoryC;
        private readonly IRepository<ContestActivityLog> _repositoryLog;
        private readonly IContestManagementService _contestMS;
        private static readonly SemaphoreSlim _semaphore = new(1, 1);

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="MainJobHandler"/>, injetando os repositórios necessários
        /// e o serviço de gerenciamento de concursos para manipular as relações entre concursos diários e concursos base.
        /// </summary>
        /// <param name="repositoryBC">Repositório para gerenciamento dos concursos base.</param>
        /// <param name="repositoryC">Repositório para gerenciamento dos concursos diários.</param>
        /// <param name="repositoryLog">Repositório responsável pelo registro de atividades dos concursos.</param>
        /// <param name="contestMS">Serviço para manipulação de concursos, incluindo operações como interseção de números.</param>
        public MainJobHandler(
            IBaseContestRepository repositoryBC, 
            IContestRepository repositoryC,  
            IContestManagementService contestMS,
            IRepository<ContestActivityLog> repositoryLog)
        {
            _repositoryBC = repositoryBC;
            _repositoryC = repositoryC;
            _contestMS = contestMS;
            _repositoryLog = repositoryLog;
        }

        /// <summary>
        /// Executa a lógica principal do job, comparando concursos diários com concursos base,
        /// estabelecendo relações e registrando atividades dos concursos.
        /// Utiliza um semáforo para garantir a execução thread-safe.
        /// </summary>
        /// <returns>Uma tarefa que representa a operação assíncrona.</returns>
        public async Task ExecuteAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                Log.Information("Iniciando execução do MainJobHandler...");

                await SaveRelationshipsAsync();

                Log.Information("Finalizando execução do MainJobHandler...");

            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro durante a execução do MainJobHandler");
            }
            finally
            {
                _semaphore.Release();
                Log.Debug("Recurso liberado.");
            }
        }

        /// <summary>
        /// Compara os concursos diários com os concursos base, calcula a interseção dos números,
        /// e atualiza as relações e os registros de atividades.
        /// Salva relações quando a interseção excede 10 e registra a atividade com detalhes da correspondência.
        /// </summary>
        /// <returns>Uma tarefa que representa a operação assíncrona.</returns>
        private async Task SaveRelationshipsAsync()
        {
            // Foi necessário criar repositórios específicos para recuperar as listas de ambas as entidades.
            // O método do repositório genérico não possuía o include das listas e utilizava AsNoTracking,
            // impossibilitando o update direto. 
            // Assim, o método genérico é mantido apenas para listagens simples,
            // enquanto os repositórios específicos permitem atualizações e consultas mais detalhadas.
            var baseContests = await _repositoryBC.GetAllWithContestsAbove11Async();

            var contests = await _repositoryC.GetAllWithBaseContestsAsync();

            int allHits = 0;

            if (baseContests.Any() && contests.Any())
            {
                foreach (var x in baseContests)
                {
                    var numbersBC = _contestMS.ConvertFormattedStringToList(x.Numbers);

                    foreach (var y in contests)
                    {
                        var numbersC = _contestMS.ConvertFormattedStringToList(y.Numbers);

                        allHits = 0;

                        if (y.LastProcessed == null || y.LastProcessed < x.CreatedAt)
                        {
                            allHits = _contestMS.CalculateIntersection(numbersBC, numbersC);

                            switch (allHits)
                            {
                                case 11: x.AddHit11(); break;
                                case 12: x.AddHit12(); break;
                                case 13: x.AddHit13(); break;
                                case 14: x.AddHit14(); break;
                                case 15: x.AddHit15(); break;
                            }

                            if (allHits > 10 && !x.ContestsAbove11.Contains(y))
                            {
                                var log = new ContestActivityLog(
                                    y.Name,
                                    y.Numbers,
                                    y.Data,
                                    x.Name,
                                    x.Numbers,
                                    allHits);

                                await _repositoryLog.AddAsync(log);

                                x.ContestsAbove11.Add(y);
                                y.BaseContests.Add(x);
                                await _repositoryC.UpdateContestAsync(y);
                                Log.Debug("O concurso {Name} teve mais de 11 acertos", y.Name);
                            }
                        }
                        else
                        {
                            Log.Debug("Concurso {Name}: já está atualizado", y.Name);
                        }
                    }
                    await _repositoryBC.UpdateBaseContestAsync(x);
                }

                foreach (var y in contests)
                {
                    y.LastProcessed = DateTime.Now;
                    await _repositoryC.UpdateContestAsync(y);
                }

                }
            else
            {
                Log.Warning("Não tem registro na tabela Concurso e/ou Concurso Base.");
            }
        }
    }
}