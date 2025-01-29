

using Biblioteca.Presentation.Components;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Timers;

namespace Biblioteca.Data.Models
{
    public class Book
    {
        [Key]
        [Required(ErrorMessage ="El código es requerido")]
        [Length(13,13,ErrorMessage ="El código debe de ser de 13 dígitos")]
        
        public string Id_Book { get; set; }

        [Required(ErrorMessage ="El Título es requerido")]
        [MinLength(3,ErrorMessage ="El Titulo debe de ser mayor a 2 dígitos")]
        public string Title { get; set; }
        [Required(ErrorMessage ="La cantidad de páginas son requeridas")]
        public string Pages { get; set; }
        [Required(ErrorMessage ="El precio del libro es requerido")]
        [Range(500,10000,ErrorMessage ="El precio del libro debe de estar entre 500 a 10000 colones")]
        public double Price { get; set; }
        [Required(ErrorMessage ="La fecha de edición es requerida")]
        
        public DateTime EditionDate { get; set; }

        //Relación con el Author
        [Required(ErrorMessage ="El autor es requerido")]
        public string Author_id { get; set; }
        public Author Author { get; set; }
        public int Status_id { get; set; }
        public Status Status { get; set; }


        //Hacer dos reportes
        //Agregar cantidad de libros
        //Reporte por fecha
        //Reportes de Un autor.Que libros tiene pablo
    }
}
