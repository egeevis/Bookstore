using BookStore.Api.Data;
using Microsoft.EntityFrameworkCore;

// BookStore.Api -> Program.cs
var builder = WebApplication.CreateBuilder(args);

// 1. CORS Politikasını Tanımla
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowGitHub",
        policy =>
        {
            policy.WithOrigins("https://egeevis.github.io") // Sadece domain adı
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

builder.Services.AddControllers();

// Veritabanı bağlantını da buraya eklemeyi unutma
builder.Services.AddDbContext<BookContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// 2. CORS'u Aktifleştir (Burası Çok Kritik: Sıralama Önemli!)
// UseHttpsRedirection ve UseAuthorization'dan HEMEN ÖNCE olmalı
app.UseCors("AllowGitHub");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DÜZELTME: Veritabaný servisini buraya, Build iþleminden ÖNCEYE aldýk.
builder.Services.AddDbContext<BookStoreContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// 2. Tanýmladýðýmýz politikayý devreye sok
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
