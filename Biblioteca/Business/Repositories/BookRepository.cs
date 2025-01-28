using Biblioteca.Business.Interfaces;
using Biblioteca.Data;
using Biblioteca.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Business.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDBContext _context;
        public BookRepository(AppDBContext context) {
            _context = context;
        
        }
        public async Task<Book> AddBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
           await _context.SaveChangesAsync();
            return book;
        }

        public async Task DeleteBookAsync(string id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null) {
                 _context.Books.Remove(book);
                await _context.SaveChangesAsync();

            }

        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.Include(a=>a.Author).ToListAsync();
        }

        //Cuando quiero que el libro sea null :     public async Task<Book?> GetBookByIdAsync(string id)
        public async Task<Book> GetBookByIdAsync(string id)
        {
            var book= await _context.Books.Include(a => a.Author).FirstOrDefaultAsync(b =>b.Id_Book==id);
            if (book == null)
            {
                throw new KeyNotFoundException($"No se encontró el libro deseado con " +
                    $"el ID {id}");
            }
              return book;
            
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
    }
}
