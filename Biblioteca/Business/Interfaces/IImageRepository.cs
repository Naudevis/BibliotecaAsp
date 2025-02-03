namespace Biblioteca.Business.Interfaces
{
    public interface IImageRepository
    {
        Task<string> SaveImageAsync(IFormFile imageFile);
    }
}
