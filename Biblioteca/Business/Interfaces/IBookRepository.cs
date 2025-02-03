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
        Task DeleteBookAsync(Book book);
        Task<IEnumerable<Book>> SearchBooks(string data);
        byte[] GeneratePdfReport(List<Book> books);
        Task<List<Book>> GetBooksMoreOldAsync();
        Task<IEnumerable<Book>> SearchByAuthorBooks(string data);
        byte[] GeneratePdfReportAuthor(List<Book> books);

    }
}
