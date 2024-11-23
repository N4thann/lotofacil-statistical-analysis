using Lotofacil.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotofacil.Application.BackgroundJobs
{
    public class MainJobHandler : IJobHandler//Todos os BackgroundJobs devem implementar essa interface
    {
        public async Task ExecuteAsync()
        {
            // Lógica do job
            Console.WriteLine("Executando job: MyJobHandler");
            await Task.Delay(1000); // Simulação de processamento
        }
    }
}
