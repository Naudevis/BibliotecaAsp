using Biblioteca.Business.Interfaces;
using Biblioteca.Data;
using Biblioteca.Data.Models;
using Biblioteca.Presentation.Components.Pages;
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
            author.Status_id = 1;
           await  _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task DeleteAuthorAsync(Author author)
        {
            var ExistAuthor = await _context.Authors.FindAsync(author.Author_id);
            if (ExistAuthor != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }

        }

       

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
          return  await _context.Authors.Include(b=>b.Books).Include(s =>s.StatusAuthor).Where(s => s.Status_id == 1).OrderBy(a =>a.Name).Take(4).ToListAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(string Author_id)
        {
            var author  = await _context.Authors.Include(b => b.Books).FirstOrDefaultAsync(a =>a.Author_id == Author_id);

            if (author == null)
            {
                return author = new();
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



        public async Task<IEnumerable<Author>> FindAuthors(string data)
        {

            if (data == "*")
            {
                return await _context.Authors.Include(b => b.Books).Include(s => s.StatusAuthor).Where(s => s.Status_id == 1).OrderBy(a => a.Name).ToListAsync();
            }
            IQueryable<Author> query = _context.Authors.AsQueryable();
            query = query.Include(b => b.Books).Include(s => s.StatusAuthor).Where(p => 
            p.Author_id.Contains(data) ||
             p.Name.Contains(data) ||
              p.Age.Contains(data) ||
               p.Books.Count.ToString().Contains(data) ||
               p.Biography.Contains(data) 
            && p.Status_id != 2).OrderBy(p=>p.Name).Take(4); // Filtra por código

           
            return await query.ToListAsync(); // Ejecuta la consulta y retorna el resultado

        }
    }
}


   

