using Forgeborn.Server.Data;
using Forgeborn.Server.Hubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy
            .WithOrigins("http://localhost:3000","http://forgeborn-ui:3000", "https://forgeborn.graknet.ca")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ??
                       builder.Configuration.GetConnectionString("forgebornDB") ??
                       throw new InvalidOperationException("DB_CONNECTION_STRING is missing");

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
    );

builder.Services.AddHealthChecks()
    .AddMySql(
        connectionString: connectionString,
        name: "forgebornDB",
        failureStatus: HealthStatus.Unhealthy,
        timeout: TimeSpan.FromSeconds(3),
        tags: new[] { "database", "critical" }
    );

builder.Services.AddSignalR();

var app = builder.Build();

app.MapHealthChecks("/health");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapHub<CharacterHub>("/hubs/characters");
app.MapControllers();

app.Run();
