using Biblioteca.Business.Interfaces;
using Biblioteca.Data;
using Biblioteca.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace Biblioteca.Business.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
      
        private readonly AppDBContext _context;

        public AuthorRepository(AppDBContext context)
        {
            _context = context;
            
        }
        public async Task<Author> AddAuthorAsync(Author author)
        {
           await  _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task DeleteAuthorAsync(string Author_id)
        {
            var author = await _context.Authors.FindAsync(Author_id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
          return  await _context.Authors.Include(b=>b.Books).ToListAsync();
        }

        public async Task<Author?> GetAuthorByIdAsync(string Author_id)
        {
            var author  = await _context.Authors.Include(b => b.Books).FirstOrDefaultAsync(a =>a.Author_id == Author_id);

            if (author == null)
            {
                throw new KeyNotFoundException($"No se encontró el libro deseado con " +
                    $"el ID {Author_id}");
            }
            return author;
        }

        public async Task UpdateAuthorAsync(Author author)
        {
             _context.Authors.Update(author);
            await _context.SaveChangesAsync();
        }
    }
}
