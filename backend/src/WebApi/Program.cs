using Microsoft.EntityFrameworkCore;
using MultiNinja.Backend.Infrastructure;
using MultiNinja.Backend.Infrastructure.ReadsRepository.EfCore;
using MultiNinja.Backend.Infrastructure.WritesRepository.EfCore;
using MultiNinja.Backend.WebApi.Endpoints;
using MultiNinja.Backend.WebApi.Orchestration;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

builder.Services
    .AddInfrastructure()
    .AddOrchestration()
    .AddHostedService<MultiNinja.Backend.WebApi.WriteModelProcessing.WriteModelProcessor>();

builder.Services
    .AddDbContext<WriteContext>(options =>
    {
        options.UseMySQL(
            builder.Configuration.GetConnectionString("WritesDatabase")!);
    });

builder.Services
    .AddDbContext<ReadsContext>(options =>
    {
        options.UseMySQL(
            builder.Configuration.GetConnectionString("ReadsDatabase")!);
    });

builder.Services.AddOpenApi();

var app = builder.Build();

app
    .MapAuth()
    .MapOpenApi();
app
    .MapScalarApiReference();

app.Run();

namespace MultiNinja.Backend.WebApi
{
    public sealed partial class WebApiProgram
    {
    }
}