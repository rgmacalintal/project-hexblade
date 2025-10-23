using Forgeborn.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy
            .WithOrigins("http://localhost:3000")
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

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowReact");
app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
