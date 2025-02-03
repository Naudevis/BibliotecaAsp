using Biblioteca.Business.Interfaces;
using Biblioteca.Data;

namespace Biblioteca.Business.Repositories
{
    public class ImageRepository: IImageRepository
    {

        private readonly IWebHostEnvironment _env;
        private readonly AppDBContext _context;

        public ImageRepository(IWebHostEnvironment env, AppDBContext context)
        {
            _env = env;
            _context = context;
        }
        public async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                // Genera un nombre único para la imagen
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(_env.WebRootPath, "images", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                return "images/" + fileName;  // Ruta relativa
            }

            return null;
        }
    }
}
