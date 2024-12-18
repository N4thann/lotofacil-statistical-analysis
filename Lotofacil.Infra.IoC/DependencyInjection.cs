using Lotofacil.Application.Services.Interfaces;
using Lotofacil.Application.Services;
using Lotofacil.Domain.Interfaces;
using Lotofacil.Infra.Data.Context;
using Lotofacil.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Lotofacil.Application.Validators;
using Lotofacil.Application.ViewsModel;
using Hangfire;
using Lotofacil.Application.BackgroundJobs;
using Hangfire.SqlServer;

namespace Lotofacil.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
           IConfiguration configuration)
        {
            // Pegando a string de conexão do appsettings.json
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Registrando o DbContext com a string de conexão correta
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            //Faz uma limpeza nas tabelas que o Hangfire criou no banco de dados
            //Por padrão, os jobs são marcados para expiração após 24 horas.
            GlobalConfiguration.Configuration
                .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
                {
                    JobExpirationCheckInterval = TimeSpan.FromHours(1), // Intervalo de limpeza
                    QueuePollInterval = TimeSpan.FromSeconds(15) // Frequência de verificação da fila
                });

            // Registrando serviços da camada de aplicação
            services.AddScoped<IBaseContestService, BaseContestService>();
            services.AddScoped<IContestService, ContestService>();
            services.AddScoped<IContestActivityLogService, ContestActivityLogService>();
            services.AddTransient<IContestManagementService, ContestManagementService>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IBaseContestRepository, BaseContestRepository>();
            services.AddScoped<IContestRepository, ContestRepository>();

            //Registrando serviços de validação com Fluent Validations
            services.AddTransient<IValidator<ContestViewModel>, ContestValidator>();

            //Registrando serviços relacionados aos BackgroundJobs
            services.AddScoped<JobService>();
            services.AddTransient<IJobHandler, MainJobHandler>();

            return services;
        }
    }
}
/*Dentro do Clean Architecture, cada camada depende da camada de baixo nível apenas por meio de abstrações ou interfaces. 
 * A camada de infraestrutura (Infra.Data) contém a implementação concreta do DbContext e outras dependências de dados. 
 * A camada Infra.IoC, por sua vez, expõe essas dependências por meio da classe DependencyInjection, 
 * para que elas possam ser injetadas nas camadas superiores sem a necessidade de acoplamento direto.*/

/*Usar Singleton para um serviço que depende de DbContext (ou de repositórios que usam DbContext) geralmente causa problemas, pois o DbContext não é seguro para uso simultâneo (thread-safe). 
 * Como ele mantém o estado das entidades carregadas, compartilhar a mesma instância entre várias requisições pode levar a erros e comportamentos inesperados. 
 * O Scoped, por outro lado, cria uma instância isolada do DbContext para cada requisição, evitando esses conflitos*/
