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
            book.Status_id = 1;
            await _context.Books.AddAsync(book);
           await _context.SaveChangesAsync();
            return book;
        }

        public async Task DeleteBookAsync(Book book)
        {
            var ExistBook = await _context.Books.FindAsync(book.Id_Book);
            if (ExistBook != null) {
                book.Status_id = 2;
                 _context.Books.Update(book);
                await _context.SaveChangesAsync();

            }

        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.Include(a=>a.Author).Include(s =>s.Status).Where(s=>s.Status_id==1).ToListAsync();
        }

        //Cuando quiero que el libro sea null :     public async Task<Book?> GetBookByIdAsync(string id)
        public async Task<Book> GetBookByIdAsync(string id)
        {
            var book= await _context.Books.Include(a => a.Author).FirstOrDefaultAsync(b =>b.Id_Book==id);
            if (book == null)
            {
                return book=new ();
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

        public async Task<IEnumerable<Book>> GetBookByColumnAndId(string data, string column)
        {
            IQueryable<Book> query = _context.Books.AsQueryable();

            switch (column)
            {
                case "Id":
                    query = query.Include(b => b.Author).Include(s => s.Status).Where(p => p.Id_Book.Contains(data) && p.Status_id!=2); // Filtra por código
                    break;

                case "Page":
                    query = query.Include(b => b.Author).Include(s => s.Status).Where(p => p.Pages.Contains(data) && p.Status_id != 2); // Filtra por tipo de institución
                    break;

                case "Price":
                    query = query.Include(b => b.Author).Include(s => s.Status).Where(p => p.Price.ToString().Contains(data) && p.Status_id != 2); // Filtra por nombre de institución
                    break;
                case "AuthorName":
                    query = query.Include(b => b.Author).Include(s => s.Status).Where(p => p.Author.Name.Contains(data) && p.Status_id != 2); // Filtra por tipo de institución
                    break;

                case "Title":
                    query = query.Include(b => b.Author).Include(s => s.Status).Where(p => p.Title.Contains(data) && p.Status_id != 2); // Filtra por nombre de institución
                    break;

                default:
                    // Si no se corresponde con ninguno de los casos, no se aplica ningún filtro.
                    break;
            }
            return await query.ToListAsync(); // Ejecuta la consulta y retorna el resultado

        }
    }

}
