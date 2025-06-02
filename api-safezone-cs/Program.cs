using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using api_safezone_cs.Data.AppData;
using api_safezone_cs.Repositories.Interfaces;
using api_safezone_cs.Repositories.Repository;
using api_safezone_cs.Services.Interfaces;
using api_safezone_cs.Services.Service;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var host = Environment.GetEnvironmentVariable("ORACLE_HOST");
    var user = Environment.GetEnvironmentVariable("ORACLE_USER");
    var password = Environment.GetEnvironmentVariable("ORACLE_PASSWORD");

    var baseConnectionString = builder.Configuration.GetConnectionString("OracleSQLConnection");
    var connectionString = baseConnectionString
        .Replace("{ORACLE_HOST}", host)
        .Replace("{ORACLE_USER}", user)
        .Replace("{ORACLE_PASSWORD}", password);

    options.UseOracle(connectionString);
});

builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Safezone API - Adaptive Dialogs",
        Version = "v1",
        Description = "API desenvolvida para o projeto Safezone by Adaptive Dialogs.",
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });
});

builder.Services.AddRateLimiter(rateLimiterOptions => rateLimiterOptions 
    .AddFixedWindowLimiter(policyName: "default", options =>
    {
        options.PermitLimit = 10;
        options.Window = TimeSpan.FromMinutes(1);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 0;
    })
);

builder.Services.AddScoped<IAlertaRepository, AlertaRepository>();
builder.Services.AddScoped<IOcorrenciaRepository, OcorrenciaRepository>();
builder.Services.AddScoped<IVitimaRepository, VitimaRepository>();

builder.Services.AddScoped<IAlertaService, AlertaService>();
builder.Services.AddScoped<IOcorrenciaService, OcorrenciaService>();
builder.Services.AddScoped<IVitimaService, VitimaService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Safezone API - Adaptive Dialogs V1");
        options.DocumentTitle = "Safezone API - Adaptive Dialogs";
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.UseRateLimiter();

app.Run();