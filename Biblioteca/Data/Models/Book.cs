﻿

using Biblioteca.Presentation.Components;
using Biblioteca.Presentation.Components.Pages;
using SixLabors.ImageSharp.ColorSpaces;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Timers;

namespace Biblioteca.Data.Models
{
    public class Book
    {
        [Key]
        [Required(ErrorMessage ="El código es requerido.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El código debe contener solo números, no se permiten letras.")]
        [Length(13,13,ErrorMessage ="El código debe de ser de 13 dígitos.")]
        
        public string Id_Book { get; set; }

        [Required(ErrorMessage ="El Título es requerido")]
        [MinLength(3,ErrorMessage ="El Titulo debe de ser mayor a 2 dígitos.")]
        public string Title { get; set; }
        [Required(ErrorMessage ="La cantidad de páginas son requeridas.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Las páginas deben de ser solo números.")]
        public string Pages { get; set; }

        [Required(ErrorMessage = "La cantidad de libros es requerida.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Las cantidad de libros deben de ser solo números.")]
        [Range(1,200, ErrorMessage = "Las cantidad de libros deben de ser mayor a 0.")]
        public int Stock { get; set; }
        [Required(ErrorMessage ="El precio del libro es requerido")]

        [Range(500,10000,ErrorMessage ="El precio del libro debe de estar entre 500 a 10000 colones")]
    
        public double Price { get; set; }
        [Required(ErrorMessage ="La fecha de edición es requerida")]
        
        public DateTime EditionDate { get; set; }

        public DateTime CreateDate { get; set; }
        public string? ImagePathBook { get; set; }

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
