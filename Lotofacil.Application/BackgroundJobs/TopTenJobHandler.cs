using Lotofacil.Application.Services;
using Lotofacil.Application.Services.Interfaces;
using Lotofacil.Domain.Interfaces;

namespace Lotofacil.Application.BackgroundJobs
{
    /// <summary>
    /// Responsável por executar o job que analisa os 10 números mais frequentes em um concurso base, 
    /// iterando pelos concursos associados. O resultado é armazenado na propriedade 
    /// <see cref="BaseContest.TopTenNumbers"/>, podendo ser utilizado para atualizações em tempo real em dashboards.
    /// </summary>
    public class TopTenJobHandler
    {
        private readonly IBaseContestService _baseContestService;
        private readonly IContestManagementService _contestMS;
        private readonly IBaseContestRepository _repositoryBC;
        private static readonly SemaphoreSlim _semaphore = new(1, 1);

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="TopTenJobHandler"/>, 
        /// injetando os serviços e o repositório necessários para gerenciar os concursos base 
        /// e suas relações com outros concursos.
        /// </summary>
        /// <param name="repositoryBC">Repositório responsável pelo gerenciamento dos concursos base.</param>
        /// <param name="contestMS">Serviço que manipula operações relacionadas a concursos.</param>
        /// <param name="baseContestService">Serviço para obtenção de concursos base com concursos associados.</param>
        public TopTenJobHandler(IBaseContestRepository repositoryBC,
            IContestManagementService contestMS,
            IBaseContestService baseContestService)
        {
            _repositoryBC = repositoryBC;
            _contestMS = contestMS;
            _baseContestService = baseContestService;
        }

        /// <summary>
        /// Executa o job que calcula os 10 números mais frequentes para cada concurso base.
        /// O processo percorre os concursos associados, conta a ocorrência dos números e atualiza
        /// a propriedade <see cref="BaseContest.TopTenNumbers"/>.
        /// O uso de um semáforo garante que a execução seja thread-safe, impedindo concorrência indesejada.
        /// </summary>
        /// <returns>Uma tarefa que representa a operação assíncrona.</returns>
        public async Task ExecuteAsync()
        {
           await _semaphore.WaitAsync();
            try
            {
                // Obter todos os concursos base
                var baseContests = await _baseContestService.GetAllWithContestsAbove11Async();

                foreach (var baseContest in baseContests)
                {
                    var occurrences = Enumerable.Range(1, 25).ToDictionary(i => i, _ => 0);//Substitui um for tradicional para preencher o valor do dictionary

                    // Contabilizar ocorrências dos números nos concursos
                    foreach (var subContest in baseContest.ContestsAbove11)
                    {
                        var numbers = _contestMS.ConvertFormattedStringToList(subContest.Numbers);
                        foreach (var number in numbers)
                        {
                            occurrences[number]++;
                        }
                    }

                    // Obter os 10 números mais frequentes, mantendo apenas as chaves (números)
                    var top10Numbers = occurrences
                        .OrderByDescending(x => x.Value) // Ordena pela frequência (maior primeiro)
                        .ThenBy(x => x.Key) // Em caso de empate, ordena pelo menor número
                        .Take(10)
                        .Select(x => x.Key) // Pega apenas os números
                        .OrderBy(x => x) // Ordena em ordem crescente
                        .Select(x => x.ToString("D2")) // Formata para "01", "02", etc.
                        .ToList();

                    //Chama um método da entidade para atribuir valor a variável TopTenNumbers
                    baseContest.AddTopTenNumbers(string.Join("-", top10Numbers));

                    // Salvar alterações no serviço
                    await _repositoryBC.UpdateBaseContestAsync(baseContest);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar o job: {ex.Message}");
            }
            finally
            {
                _semaphore.Release();
                Console.WriteLine("Recurso liberado.");
            }
        }
    }
}
