using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotofacil.Application.Services.Interfaces
{
    public interface IJobHandler
    {
        Task ExecuteAsync();
    }
}
/*Define a interface ou contrato para execução de tarefas. 
 * É uma abstração que garante que diferentes tipos de jobs sigam uma estrutura uniforme.
 */
