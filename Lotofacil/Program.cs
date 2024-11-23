
using System.Configuration;
using Lotofacil.Infra.IoC;
using Lotofacil.Infra.Data.Context;//tirar pois feri a Clean Architecture
using Microsoft.EntityFrameworkCore;
using Hangfire;
using Lotofacil.Application.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//M�todo configurado na camada Infra.IoC
builder.Services.AddInfrastructure(builder.Configuration);

/*Inicializar o servidor e o dashboard. Essa configura��o � feita no pipeline de inicializa��o da aplica��o, 
 * uma vez que est� relacionado ao ciclo de vida do aplicativo.*/
builder.Services.AddHangfire(config =>
{
    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
          .UseSimpleAssemblyNameTypeSerializer()
          .UseRecommendedSerializerSettings()
          .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddHangfireServer();// Inicializa o servidor do Hangfire


var app = builder.Build();

// Aplique migra��es automaticamente durante o startup
using (var scope = app.Services.CreateScope())
{
    //CreateScope(): Cria um escopo para resolver servi�os dentro do ciclo de vida da aplica��o.
    //Isso � necess�rio para que voc� consiga injetar o ApplicationDbContext e trabalhar com ele.
    var services = scope.ServiceProvider;

    try
    {
        // Obt�m uma inst�ncia do seu ApplicationDbContext
        var context = services.GetRequiredService<ApplicationDbContext>();

        //Migra��es autom�ticas: O c�digo context.Database.Migrate();
        //garante que, se houver migra��es pendentes, elas sejam aplicadas automaticamente ao iniciar a aplica��o.
        //Se o seu projeto utiliza Entity Framework para gerenciar o esquema do banco de dados, isso � �til.
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        // Faz o log do erro, se ocorrer
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro na migra��o ou ao alimentar o banco de dados.");
    }
}

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
        "*/1 * * * *"); // Executa a cada 10 minutos
}

app.Run();
