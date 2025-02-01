using Biblioteca.Business.Interfaces;
using Biblioteca.Data;
using Biblioteca.Data.Models;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

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
            return await _context.Books.Include(a => a.Author).Include(s => s.Status).Where(s => s.Status_id == 1).OrderBy(b => b.Title).Take(4).ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(string id)
        {
            var book = await _context.Books.Include(a => a.Author).FirstOrDefaultAsync(b => b.Id_Book == id);
            if (book == null)
            {
                return book = new();
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

        public async Task<IEnumerable<Book>> SearchBooks(string data)
        {
            if (data == "*")
            {
                return await _context.Books.Include(a => a.Author).Include(s => s.Status).Where(s => s.Status_id == 1).OrderBy(b => b.Title).ToListAsync();
            }
            IQueryable<Book> query = _context.Books.AsQueryable();
            query = query.Include(b => b.Author).Include(s => s.Status).Where(p =>
            p.Id_Book.Contains(data) ||
            p.Pages.Contains(data) ||
             p.Stock.ToString().Contains(data) ||
            p.Price.ToString().Contains(data) ||
            p.Author.Name.Contains(data) ||
            p.Title.Contains(data) &&
            p.Status_id != 2).OrderBy(b => b.Title).Take(4); // Filtra por código


            return await query.ToListAsync(); // Ejecuta la consulta y retorna el resultado

        }
        public async Task<List<Book>> GetBooksAsync()
        {
            // Este ejemplo usa Entity Framework para obtener los libros de la base de datos
            var person = await _context.Books.Include(b => b.Author).Where(b=>b.Status_id==1).GroupBy(b=>b.Author_id).ToListAsync();
            Console.WriteLine(person[1].FirstOrDefault().Author_id);
                
            return await _context.Books.Include(b => b.Author).Where(b=>b.Author_id== person[1].FirstOrDefault().Author_id).ToListAsync();
                
        }


        public byte[] GeneratePdfReport(List<Book> books)
        {
            var document = new PdfDocument();

            var page = document.AddPage();

            var graphics = XGraphics.FromPdfPage(page);

           
            var titleFont = new XFont("Arial", 20, XFontStyle.Bold);   
            var headerFont = new XFont("Arial", 12, XFontStyle.Bold);  
            var bodyFont = new XFont("Arial", 12, XFontStyle.Regular); 
            var ReportTitle = new XFont("Arial", 14, XFontStyle.Bold);


            var logoPath = "wwwroot/images/PdfCabecera.jpg"; 
            if (File.Exists(logoPath))
            {
                var logo = XImage.FromFile(logoPath);
                graphics.DrawImage(logo, 0, 0, 650, 80);  
            }

            var color = XColor.FromArgb(0x59, 0x69, 0x6B); 


            var header = new XSolidBrush(color);
            
            graphics.DrawString("Biblioteca de San Pedro", titleFont, XBrushes.White, new XPoint(180, 50));

            var HeaderColor = XColor.FromArgb(0x0F, 0x20, 0x6D);
            var lineTable = XColor.FromArgb(0x1D, 0x21, 0x21);
            var lineColor = new XSolidBrush(lineTable);

            var headerTitle = new XSolidBrush(HeaderColor);
                                                           
                                                           
            
            var logoUpalaPath = "wwwroot/images/escudo.jpg"; 
            if (File.Exists(logoUpalaPath))
            {
                var logo = XImage.FromFile(logoUpalaPath);
                graphics.DrawImage(logo, 60, 95, 80, 80);  
            }

            var date = DateTime.Now;
            graphics.DrawString("Dirección: San Pedro, San José, Upala, Alajuela", headerFont, XBrushes.Gray, new XPoint(295, 105));
            graphics.DrawString("Email: raudyncamacho3@gmail.com", headerFont, XBrushes.Gray, new XPoint(295, 125));
            graphics.DrawString("Fecha de emisión: " + date, headerFont, XBrushes.Gray, new XPoint(295, 145));
            graphics.DrawString("Teléfono: 87725315", headerFont, XBrushes.Gray, new XPoint(295, 165));

            graphics.DrawString("Reporte del autor con más libros en Stock", ReportTitle, XBrushes.Gray, new XPoint(25, 220));

            graphics.DrawRectangle(headerTitle, new XRect(20, 240, 100, 20));
            graphics.DrawRectangle(headerTitle, new XRect(120, 240, 200, 20));
            graphics.DrawRectangle(headerTitle, new XRect(320, 240, 80, 20));
            graphics.DrawRectangle(headerTitle, new XRect(400, 240, 175, 20));

            graphics.DrawString("Código", headerFont, XBrushes.White, new XPoint(50, 248 + 5));
            graphics.DrawString("Título", headerFont, XBrushes.White, new XPoint(200, 248 + 5));
            graphics.DrawString("Stock", headerFont, XBrushes.White, new XPoint(340, 248 + 5));
            graphics.DrawString("Autor", headerFont, XBrushes.White, new XPoint(467, 248 + 5));

           
            int yPosition = 278; 
            double BookIdColumnWidth =100;
            double authorColumnWidth = 175;
            double StockColumnWidth = 80;
            double TitleColumnWidth = 190;


            foreach (var book in books)
            {
        
                var BookIdText = book.Id_Book;
                var authorText = book.Author.Name;
                var TitleText = book.Title;
                var StockText = book.Stock.ToString();
                var bookIdLines = WordWrapText(BookIdText, BookIdColumnWidth, bodyFont, graphics);
                var authorLines = WordWrapText(authorText, authorColumnWidth, bodyFont, graphics);
                var titleLines = WordWrapText(TitleText, TitleColumnWidth, bodyFont, graphics);
                var stockLines = WordWrapText(StockText, StockColumnWidth, bodyFont, graphics);
                List<int> minYPosition = [authorLines.Count(), titleLines.Count(), stockLines.Count(), bookIdLines.Count()];



                double currentYPositionAuthor = yPosition +  (15 * ((minYPosition.Max() - authorLines.Count()) / 2));
                double currentYPositionBookId = yPosition + (15 * ((minYPosition.Max() - bookIdLines.Count()) / 2));
                double currentYPositionTitle = yPosition;
                double currentYPositionStock = yPosition + (15 * ((minYPosition.Max() - stockLines.Count()) / 2));

                foreach (var line in bookIdLines)
                {
                    graphics.DrawString(line, bodyFont, lineColor, new XPoint(25, currentYPositionBookId));
                    currentYPositionBookId += 15; 

                }

                foreach (var line in titleLines)
                {
                    graphics.DrawString(line, bodyFont, lineColor, new XPoint(125, currentYPositionTitle));
                    currentYPositionTitle += 15;

                }
                foreach (var line in authorLines)
                {
                    graphics.DrawString(line, bodyFont, lineColor, new XPoint(415, currentYPositionAuthor));
                    currentYPositionAuthor += 15; 

                }


                foreach (var line in stockLines)
                {
                    graphics.DrawString(line, bodyFont, lineColor, new XPoint(335, currentYPositionStock));
                    currentYPositionStock += 15; 

                }
                List<double> mayor = [currentYPositionTitle, currentYPositionAuthor, currentYPositionStock];


               
                yPosition = ((minYPosition.Max()*15)-15)+ yPosition;

                graphics.DrawLine(XPens.Gray, 21, yPosition + 4, 574, yPosition + 4);
                graphics.DrawLine(XPens.Gray, 21, yPosition- (18 + ((minYPosition.Max() * 15) - 15)), 21, yPosition + 4);
                graphics.DrawLine(XPens.Gray, 119, yPosition- (18 + ((minYPosition.Max() * 15) - 15)), 119, yPosition + 4);
                graphics.DrawLine(XPens.Gray, 319, yPosition- (18 + ((minYPosition.Max() * 15) - 15)), 319, yPosition + 4);
                graphics.DrawLine(XPens.Gray, 399, yPosition - (18 + ((minYPosition.Max() * 15) - 15)), 399, yPosition + 4);
                graphics.DrawLine(XPens.Gray, 574, yPosition- (18 + ((minYPosition.Max() * 15) - 15)), 574, yPosition + 4);

                yPosition += 21; 
            }

            
            using (var stream = new MemoryStream())
            {
                
                document.Save(stream);

               
                return stream.ToArray();
            }
        }


        private List<string> WordWrapText(string text, double maxWidth, XFont font, XGraphics graphics)
        {
            var words = text.Split(' ');
            var lines = new List<string>();
            var currentLine = string.Empty;

            foreach (var word in words)
            {
                var testLine = currentLine.Length > 0 ? currentLine + " " + word : word;
                var width = graphics.MeasureString(testLine, font).Width;

                if (width <= maxWidth)
                {
                    currentLine = testLine;
                }
                else
                {
                    if (!string.IsNullOrEmpty(currentLine))
                    {
                        lines.Add(currentLine);
                    }
                   
                    currentLine = word;
                }
            }

            if (!string.IsNullOrEmpty(currentLine))
            {
                lines.Add(currentLine);
            }

            return lines;
        }

    }


}
