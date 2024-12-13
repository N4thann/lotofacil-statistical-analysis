using Lotofacil.Application.Services.Interfaces;
using Lotofacil.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lotofacil.Application.BackgroundJobs
{
    public class LogJobHandler : IJobHandler
    {
        public async Task ExecuteAsync()
        {
                Console.WriteLine("Iniciando execução do LogJobHandler...");
         
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
