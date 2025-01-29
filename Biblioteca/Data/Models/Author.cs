using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Data.Models
{
    public class Author
    {
        [Key]
        public string Author_id { get; set; }

        public string Name { get; set; }
        public string Age { get; set; }

        public string Biography { get; set; }

        public int Status_id { get; set; }
        public Status StatusAuthor { get; set; }
        //Relación con Books
        public ICollection<Book> Books { get; set; }

    }
}


//Como sacar un la coneccion a un block de notas(Json) para configurarlo desde afuera