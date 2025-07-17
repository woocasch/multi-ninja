using MultiNinja.Backend.Infrastructure;
using MultiNinja.Backend.WebApi.Endpoints;
using MultiNinja.Backend.WebApi.Orchestration;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure()
    .AddOrchestration();

var app = builder.Build();

app
    .MapAuth();

app.Run();

namespace MultiNinja.Backend.WebApi
{
    public sealed partial class WebApiProgram
    {
    }
}