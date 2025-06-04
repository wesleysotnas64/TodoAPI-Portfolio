using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using TodoAPI_Portfolio.Data;
using TodoAPI_Portfolio.Services;

var builder = WebApplication.CreateBuilder(args);

// Carrega variáveis do .env
Env.Load();
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

// Configura CORS para aceitar qualquer origem
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Serviços e contexto
builder.Services.AddScoped<TodoService>();
builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
