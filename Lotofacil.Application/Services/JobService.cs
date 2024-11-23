using Lotofacil.Application.Services.Interfaces;


namespace Lotofacil.Application.Services
{
    public class JobService
    {
        /*Um dicionário que associa o nome de cada job (como chave) ao respectivo handler 
         * (IJobHandler como valor).*/
        private readonly Dictionary<string, IJobHandler> _jobHandlers;

        public JobService(IEnumerable<IJobHandler> jobHandlers)
        {
            _jobHandlers = jobHandlers.ToDictionary(handler => handler.GetType().Name, handler => handler);
        }

        public async Task ExecuteJob(string jobName)
        {
            if (_jobHandlers.TryGetValue(jobName, out var jobHandler))
            {
                await jobHandler.ExecuteAsync();
            }
            else
            {
                throw new Exception($"Job '{jobName}' não encontrado.");
            }
        }
    }
}
/* Implementa a lógica principal associada ao trabalho do job. 
 * Ele contém a lógica de negócios específica que será executada quando o job for disparado.*/
