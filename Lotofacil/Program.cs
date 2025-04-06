using Lotofacil.Infra.IoC;
using Hangfire;
using Lotofacil.Application.BackgroundJobs;
using Lotofacil.Infra.Data.Context;
using Lotofacil.Infra.Data.Initialization;
using Lotofacil.Infra.Data.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Método configurado na camada Infra.IoC
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// ✅ SEED EXECUTADO AQUI (ANTES DE app.Run)
using (var scope = app.Services.CreateScope()) // Cria um escopo de serviço (escopo = ciclo de vida da request)
{
    // Pega o serviço registrado de IDataInitializer (a implementação concreta é ApplicationDbInitializer)
    var initializer = scope.ServiceProvider.GetRequiredService<IDataInitializer>();

    // Executa o método Seed, que vai aplicar migrações e popular o banco se necessário
    initializer.Seed();
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

// Configuração do dashboard do Hangfire
app.UseHangfireDashboard("/hangfire");

RecurringJob.AddOrUpdate<MainJobHandler>(
    "main-job",
    x => x.ExecuteAsync(),
    "*/4 * * * *");

RecurringJob.AddOrUpdate<TopTenJobHandler>(
    "Top-ten-job",
    service => service.ExecuteAsync(),
    "*/9 * * * *"); 


app.Run();
