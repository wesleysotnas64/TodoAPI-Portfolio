using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using TodoAPI_Portfolio.Data;
using TodoAPI_Portfolio.Hubs;
using TodoAPI_Portfolio.Services;

var builder = WebApplication.CreateBuilder(args);

// Carrega variáveis do .env
Env.Load();
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

// Configura CORS para aceitar apenas a origem do frontend e permitir credenciais
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins(Environment.GetEnvironmentVariable("URL_ORIGIN")) // endereço do frontend
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Adiciona SignalR
builder.Services.AddSignalR();

// Serviços e contexto
builder.Services.AddScoped<TodoService>();
builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger para dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Ativa CORS antes de autenticação/autorização e controllers
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Mapeia o Hub SignalR
app.MapHub<TodoHub>("/todoHub");

app.Run();
