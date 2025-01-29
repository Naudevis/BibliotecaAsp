﻿using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Data.Models
{
    public class Author
    {
        [Key]
        [Required(ErrorMessage ="La cédula es requerida")]
        [Length(9,15,ErrorMessage ="El número de cedula debe de ir estre 9 y 15")]
        public string Author_id { get; set; }

        [Required(ErrorMessage = "El nombre es requerida")]
        [MinLength(3, ErrorMessage = "El nombre debe de ser mayor a 2 dígitos")]
        public string Name { get; set; }
        [Required(ErrorMessage = "La edad es requerida")]
        [Range(1,150,ErrorMessage ="La edad de una persona debe de estar entre el rango de 0 a 150 años")]
        public string Age { get; set; }
        [Required(ErrorMessage = "La biografía es requerida")]
        [MinLength(10, ErrorMessage = "La biografía debe ser mayor a 15 dígitos")]
        public string Biography { get; set; }

        public int Status_id { get; set; }
        public Status StatusAuthor { get; set; }
        //Relación con Books
        public ICollection<Book> Books { get; set; }

    }
}


//Como sacar un la coneccion a un block de notas(Json) para configurarlo desde afuera