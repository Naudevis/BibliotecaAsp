using Biblioteca.Data.Models;
using System.Data.SqlTypes;

namespace Biblioteca.Business.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetBookByIdAsync(string id);
        Task<Book> AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(string id);

    }
}
