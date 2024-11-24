
using System.Configuration;
using Lotofacil.Infra.IoC;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using Lotofacil.Application.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Método configurado na camada Infra.IoC
builder.Services.AddInfrastructure(builder.Configuration);

/*Inicializar o servidor e o dashboard. Essa configuração é feita no pipeline de inicialização da aplicação, 
 * uma vez que está relacionado ao ciclo de vida do aplicativo.*/
builder.Services.AddHangfire(config =>
{
    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
          .UseSimpleAssemblyNameTypeSerializer()
          .UseRecommendedSerializerSettings()
          .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddHangfireServer();// Inicializa o servidor do Hangfire


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


// Configurar jobs recorrentes usando o Hangfire
using (var hangfireScope = app.Services.CreateScope())
{
    var recurringJobManager = hangfireScope.ServiceProvider.GetRequiredService<IRecurringJobManager>();

    recurringJobManager.AddOrUpdate<IJobHandler>(
        "MainJobHandler", // Identificador do job
        job => job.ExecuteAsync(),
        "*/10 * * * *"); // Executa a cada 10 minutos

    //recurringJobManager.AddOrUpdate<IJobHandler>(
    //    "LogsJobHandler",
    //    job => job.ExecuteAsync(),
    //    "*/1 * * * *");

}

app.Run();
