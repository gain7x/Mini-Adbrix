using Microsoft.AspNetCore.HttpOverrides;
using Search.Api.Repositories;

var logFactory = LoggerFactory.Create(config =>
{
    config.AddConsole();
    config.AddDebug();
}).CreateLogger("Program");

string connString;

try
{
    connString = Environment.GetEnvironmentVariable("DB_CONN_STR")
        ?? throw new ArgumentNullException("DB_CONN_STR not found.");
}
catch (Exception ex)
{
    logFactory.LogWarning(ex.Message);
    throw;
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IEventRepository, MySqlEventRepository>(provider => new MySqlEventRepository(connString));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.Run();
