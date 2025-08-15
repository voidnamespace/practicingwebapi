using Microsoft.EntityFrameworkCore;
using AppNum5.Models;

var builder = WebApplication.CreateBuilder(args);

// ������������ DbContext � SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=users.db"));

// ������������ ����������� (���� ����)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// �������� ��� ������������
app.MapControllers();

app.Run();
