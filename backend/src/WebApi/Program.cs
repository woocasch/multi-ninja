using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MultiNinja.Backend.Infrastructure;
using MultiNinja.Backend.Infrastructure.ReadsRepository.EfCore;
using MultiNinja.Backend.Infrastructure.WritesRepository.EfCore;
using MultiNinja.Backend.WebApi.Endpoints;
using MultiNinja.Backend.WebApi.Orchestration;
using MultiNinja.Backend.WebApi.Settings;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

builder.Services
    .AddInfrastructure()
    .AddOrchestration()
    .AddHostedService<MultiNinja.Backend.WebApi.WriteModelProcessing.WriteModelProcessor>();

builder.Services
    .AddDbContextPool<WriteContext>(options =>
    {
        options.UseMySQL(
            builder.Configuration.GetConnectionString("WritesDatabase")!);
    });

builder.Services
    .AddDbContextPool<ReadsContext>(options =>
    {
        options.UseMySQL(
            builder.Configuration.GetConnectionString("ReadsDatabase")!);
    });

var authorizationSettings = new AuthorizationSettings();
builder.Configuration.Bind("AuthorizationSettings", authorizationSettings);

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(jwtOptions =>
    {
        // jwtOptions.Authority = authorizationSettings.ServiceAddress;
        jwtOptions.Audience = authorizationSettings.Audience;
        jwtOptions.MetadataAddress = authorizationSettings.MetadataAddress;
        jwtOptions.RequireHttpsMetadata = false;
        jwtOptions.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidAudiences = [authorizationSettings.Audience],
            ValidIssuers = [authorizationSettings.Issuer],
        };

        jwtOptions.MapInboundClaims = false;
    });

var requireAuthPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

builder.Services.AddAuthorizationBuilder()
    .SetDefaultPolicy(requireAuthPolicy);

builder.Services.AddOpenApi();
builder.Services.AddCors();

var app = builder.Build();

app
    .MapAuth()
    .MapOpenApi();
app
    .MapScalarApiReference();

app.UseCors(options =>
    options
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed(_ => true)
        .AllowCredentials());

app.UseAuthentication();
app.UseAuthorization();
app.Run();

namespace MultiNinja.Backend.WebApi
{
    public sealed partial class WebApiProgram
    {
    }
}