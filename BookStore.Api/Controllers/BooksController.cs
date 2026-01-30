using BookStore.Api.Data;
using BookStore.Web.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreContext _context;
        public BooksController (BookStoreContext context)
        {
            _context = context;
        }

        [HttpGet] // 1. Tabela: Bu bir okuma işidir
        public List<Book> GetAllBooks() // 2. Dönüş Tipi: Kitap listesi döneceğim
        {
            // 3. İşlem: Depodaki kitapları listeye çevirip döndür.
            return _context.Books.ToList();
        }

        [HttpPost] // 1. Tabela: Bu bir ekleme işidir
        public Book AddBook(Book book) // 2. Parametre: Bana eklemem için bir 'book' ver
        {
            // Adım 1: Gelen kitabı rafa koy (Ama henüz veritabanına gitmedi, hafızada)
            _context.Books.Add(book);

            // Adım 2: Değişiklikleri onayla ve veritabanına yaz (KAYDET BUTONU)
            _context.SaveChanges();

            // Adım 3: Eklenen kitabı geriye dön (Başarılı olduğunu görsünler)
            return book;
        }

        [HttpGet("{id}")] // DİKKAT: Adreste ID beklediğimizi belirttik (Örn: api/Books/5)
        public Book GetBookById(int id) // Parametre olarak o ID'yi aldık
        {
            // Veritabanında (Books) bu id'ye sahip olanı BUL (Find) ve döndür.
            return _context.Books.Find(id);
        }
        [HttpPut]
        public Book UpdateBook(Book book)
        {
            var bookId = book.Id;
            var mevcutKitap = _context.Books.Find(bookId);
            if (mevcutKitap == null)
            {
                return null;
            }

            else
            {
                mevcutKitap.Id = book.Id;
                mevcutKitap.Title = book.Title;
                mevcutKitap.Author = book.Author;
                mevcutKitap.StockQty = book.StockQty;
                mevcutKitap.Price = book.Price;
                _context.SaveChanges();

                return mevcutKitap;
            }
        }

        [HttpDelete("{id}")]
        public Book DeleteBookbyId(int id)
        {
  
            var bulunanKitap = _context.Books.Find(id);

            if (bulunanKitap == null)
            {
                return null;
            }
            else
            {
                _context.Books.Remove(bulunanKitap);
                _context.SaveChanges();
                return bulunanKitap;
            }
        }


    }


}
