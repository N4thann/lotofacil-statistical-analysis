using Hangfire;
using Lotofacil.Application.BackgroundJobs;
using Lotofacil.Application.Services.Interfaces;
using Lotofacil.Infra.IoC;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Serilog usando o método de extensão
builder.Host.ConfigureSerilog();

builder.Services.AddControllersWithViews();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("🚀 APLICAÇÃO INICIADA - Serilog está funcionando!");
logger.LogWarning("⚠️ Este é um log de WARNING para teste");

// Middleware do Serilog para logging de requisições
app.UseSerilogRequestLogging();

// Inicialização do banco de dados
using (var scope = app.Services.CreateScope())
{
    var initService = scope.ServiceProvider.GetRequiredService<IInitializationDbService>();
    initService.Initialize();
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
