using BookStore.Web.Entities; // Book sınıfını bulması için
using Microsoft.EntityFrameworkCore; // DbContext özelliklerini kullanmak için

namespace BookStore.Api.Data
{
    // ": DbContext" diyerek bu sınıfın süper güçler kazanmasını sağlıyoruz
    public class BookStoreContext : DbContext
    {
        // Bu garip kod (Constructor), dışarıdan gelen ayarları (connection string gibi)
        // alıp EF Core'un ana yapısına iletir. Ezberlemene gerek yok, kalıptır.
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options)
        {
        }

        // --- GÖREVİN ---
        // Aşağıya Books tablosunu temsil eden DbSet kodunu yaz:
        public DbSet<Book> Books { get; set; }

    }
}