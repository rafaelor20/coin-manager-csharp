// Program.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Dapper;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner
builder.Services.AddControllers();
builder.Services.AddCors();

// Registrar UserService
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TransactionService>();

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<TransactionRepository>();

// Configurar a conexão com PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// Register IDbConnection with a concrete implementation
builder.Services.AddScoped<IDbConnection>(sp =>
    new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseRouting();

// Middleware para verificação de saúde
app.MapGet("/health", () => "OK!");

// Adiciona os controladores
app.MapControllers();

// Inicia a aplicação
await InitAsync(app);
app.Run();

static async Task InitAsync(WebApplication app)
{
    try
    {
        await ConnectDbAsync(app);
        Console.WriteLine("Server is listening on port " + string.Join(", ", app.Urls));
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error initializing application: " + ex.Message);
    }
}

static async Task ConnectDbAsync(WebApplication app)
{
    try
    {
        using var scope = app.Services.CreateScope();
        var dbConnection = scope.ServiceProvider.GetRequiredService<IDbConnection>();
        dbConnection.Open();
        Console.WriteLine("Database connected.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error connecting to database: " + ex.Message);
    }
}

static Task DisconnectDbAsync()
{
    // Simula o fechamento da conexão com o banco
    Console.WriteLine("Database disconnected.");
    return Task.CompletedTask;
}
