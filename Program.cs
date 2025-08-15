using Microsoft.EntityFrameworkCore;
using AppNum5.Models;

var builder = WebApplication.CreateBuilder(args);

// Регистрируем DbContext с SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=users.db"));

// Регистрируем контроллеры (если есть)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Маршруты для контроллеров
app.MapControllers();

app.Run();
