using MultiNinja.Backend.WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);
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