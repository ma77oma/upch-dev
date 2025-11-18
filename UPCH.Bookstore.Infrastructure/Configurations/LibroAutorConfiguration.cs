using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPCH.Bookstore.Domain.Entities;
using UPCH.Bookstore.Infrastructure.Data.Base;

namespace UPCH.Bookstore.Infrastructure.Configurations
{
    public class LibroAutorConfiguration : EntityConfiguration<LibroAutorEntity>
    {
        public LibroAutorConfiguration(ModelBuilder modelBuilder)
        {
            var entityBuilder = modelBuilder.Entity<LibroAutorEntity>();

            entityBuilder.ToTable("LibroAutor");

            // Clave Primaria Compuesta
            entityBuilder.HasKey(c => c.Id);

            entityBuilder.Property(la => la.AutorId).HasColumnName("AutorId");
            entityBuilder.Property(la => la.LibroId).HasColumnName("LibroId");
            entityBuilder.Property(la => la.Posicion).HasColumnName("Posicion");

            // Relación muchos a uno con Libro
            entityBuilder.HasOne(la => la.Libro)
                         .WithMany(l => l.LibroAutor)
                         .HasForeignKey(la => la.LibroId)
                         .HasConstraintName("FK_LibroAutor_Libros");

            // Relación muchos a uno con Autor
            entityBuilder.HasOne(la => la.Autor)
                         .WithMany(a => a.LibroAutor)
                         .HasForeignKey(la => la.AutorId)
                         .HasConstraintName("FK_LibroAutor_Autores");

            Configure(entityBuilder);
        }
    }
}
