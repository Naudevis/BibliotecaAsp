using Biblioteca.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Data
{
    public class AppDBContext : DbContext
    {
        // Constructor que recibe las opciones de configuración para DbContext
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        // Definición de las tablas (DbSet) correspondientes
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Status> Statuses { get; set; }

        // Configuración de las relaciones entre las entidades
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación muchos a uno entre Book y Author
            modelBuilder.Entity<Book>()
                .HasOne(a => a.Author)  // Un libro tiene un autor
                .WithMany(b => b.Books) // Un autor puede tener muchos libros
                .HasForeignKey(a => a.Author_id); // La clave foránea en el libro

            // Relación muchos a uno entre Book y Status
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Status)  // Un libro tiene un estado
                .WithMany(s => s.Books) // Un estado puede estar asociado a muchos libros
                .HasForeignKey(b => b.Status_id); // La clave foránea en el libro


            modelBuilder.Entity<Author>()
                .HasOne(a => a.StatusAuthor)  // Un libro tiene un estado
                .WithMany(s => s.Authors) // Un estado puede estar asociado a muchos libros
                .HasForeignKey(a => a.Status_id); // La clave foránea en el libro
        }
    }
}
