using Lotofacil.Application.Services;
using Lotofacil.Application.Services.Interfaces;
using Lotofacil.Domain.Interfaces;

namespace Lotofacil.Application.BackgroundJobs
{
    public class TopTenJobHandler : IJobHandler
    {
        private readonly IBaseContestService _baseContestService;
        private readonly IContestManagementService _contestMS;
        private readonly IBaseContestRepository _repositoryBC;

        public TopTenJobHandler(IBaseContestRepository repositoryBC,
            IContestManagementService contestMS,
            IBaseContestService baseContestService)
        {
            _repositoryBC = repositoryBC;
            _contestMS = contestMS;
            _baseContestService = baseContestService;
        } 

        public async Task ExecuteAsync()
        {
            try
            {
                // Obter todos os concursos base
                var baseContests = await _baseContestService.GetAllWithContestsAbove11Async();

                foreach (var baseContest in baseContests)
                {
                    // Dicionário para contar as ocorrências dos números (1 a 25)
                    var occurrences = new Dictionary<int, int>();
                    for (int i = 1; i <= 25; i++)
                    {
                        occurrences[i] = 0;
                    }

                    // Processar concursos associados
                    foreach (var subContest in baseContest.ContestsAbove11)
                    {
                        var numbers = _contestMS.ConvertFormattedStringToList(subContest.Numbers);

                        foreach (var number in numbers)
                        {
                            if (occurrences.ContainsKey(number))
                            {
                                occurrences[number]++;
                            }
                        }
                    }

                    // Obter os 7 números mais frequentes
                    var top7Numbers = occurrences
                        .OrderByDescending(x => x.Value)
                        .Take(10)
                        .Select(x => x.Key)
                        .ToList();

                    // Atualizar o atributo na entidade BaseContest
                    baseContest.TopTenNumbers = string.Join("-", top7Numbers);

                    // Salvar alterações no serviço
                    await _repositoryBC.UpdateBaseContestAsync(baseContest);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar o job: {ex.Message}");
            }
        }
        /*
        public class AtualizarIdadeService
        {
            private readonly IDiretorRepository _repository; // Repositório para acesso ao banco.

            public AtualizarIdadeService(IDiretorRepository repository)
            {
                _repository = repository;
            }

            public async Task VerificarEAtualizarIdade()
            {
                var diretores = await _repository.ObterTodosAsync();

                foreach (var diretor in diretores)
                {
                    // Verificar se a data de nascimento + idade atual corresponde ao próximo aniversário
                    var proximoAniversario = diretor.DataNascimento.AddYears(diretor.Idade + 1);

                    if (proximoAniversario <= DateTime.UtcNow &&
                        proximoAniversario.Month == DateTime.UtcNow.Month &&
                        proximoAniversario.Day == DateTime.UtcNow.Day)
                    {
                        diretor.Idade++;
                        await _repository.AtualizarAsync(diretor);
                    }
                }
            }
        }
        */

    }

}
