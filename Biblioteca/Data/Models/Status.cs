using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Data.Models
{
    public class Status
    {
        [Key]
        public  int Status_id { get; set; }
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
        public ICollection<Author> Authors { get; set; }

    }
}
