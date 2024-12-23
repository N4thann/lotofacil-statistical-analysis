
using System.Configuration;
using Lotofacil.Infra.IoC;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using Lotofacil.Application.Services.Interfaces;
using Lotofacil.Application.BackgroundJobs;
using System.Web.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Lotofacil.Application.Services;

using DocumentFormat.OpenXml.Spreadsheet;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Método configurado na camada Infra.IoC
builder.Services.AddInfrastructure(builder.Configuration);

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
