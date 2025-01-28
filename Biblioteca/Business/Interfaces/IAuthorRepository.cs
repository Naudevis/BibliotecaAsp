using Biblioteca.Data.Models;

namespace Biblioteca.Business.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author?> GetAuthorByIdAsync(string Author_id);
        Task<Author> AddAuthorAsync(Author author);
        Task UpdateAuthorAsync(Author author);
        Task DeleteAuthorAsync(string Author_id);
    }
}
