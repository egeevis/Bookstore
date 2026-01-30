using BookStore.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. CORS servisini ekle (Ýzin politikasýný tanýmla)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Kim gelirse gelsin (Blazor, Mobil, vs.)
              .AllowAnyMethod()  // GET, POST, DELETE hepsi serbest
              .AllowAnyHeader(); // Her türlü baþlýk kabul
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DÜZELTME: Veritabaný servisini buraya, Build iþleminden ÖNCEYE aldýk.
builder.Services.AddDbContext<BookStoreContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();


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
