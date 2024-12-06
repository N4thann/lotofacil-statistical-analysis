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
    }
}
