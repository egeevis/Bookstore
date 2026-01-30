using BookStore.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Veritabaný Servisi (Mutlaka builder.Build'dan ÖNCE olmalý)
builder.Services.AddDbContext<BookStoreContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. CORS Politikasý (Ýsmini tek ve net yapalým)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowApp",
        policy =>
        {
            policy.WithOrigins("https://egeevis.github.io")
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build(); // TÜM SERVÝS TANIMLARI BURADA BÝTER

// 3. Middleware Pipeline (Sýralama Önemli!)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS, Routing ve Authorization arasýna girmeli
app.UseRouting();
app.UseCors("AllowApp");

app.UseAuthorization();
app.MapControllers();

app.Run(); // DOSYADA SADECE BÝR TANE app.Run() OLMALI