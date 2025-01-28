using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Data.Models
{
    public class Book
    {
        [Key]
        public string Id_Book { get; set; }
        public string Title { get; set; }
        public string Pages { get; set; }
        public double Price { get; set; }
        public DateTime EditionDate { get; set; }


        //Relación con el Author
        public string Author_id { get; set; }
        public Author Author { get; set; }


    }
}
