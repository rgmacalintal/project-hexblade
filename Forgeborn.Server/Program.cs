using Forgeborn.Server.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll",
//        policy => policy
//            .AllowAnyOrigin()
//            .AllowAnyMethod()
//            .AllowAnyHeader());
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy
            .WithOrigins("https://localhost:49867")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var connectionString = builder.Configuration.GetConnectionString("forgebornDB");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
//var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ??
//                      builder.Configuration.GetConnectionString("forgebornDB") ??
//                       throw new InvalidOperationException("DB_CONNECTION_STRING is missing");

//builder.Services.AddDbContext<ApplicationDbContext>(options => 
//    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
//        .LogTo(Console.WriteLine, LogLevel.Information)
//        .EnableSensitiveDataLogging()
//        .EnableDetailedErrors()
//    );

var app = builder.Build();

app.UseCors("AllowReactApp");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseDefaultFiles();
app.MapStaticAssets();

app.MapFallbackToFile("/index.html");

app.Run();
