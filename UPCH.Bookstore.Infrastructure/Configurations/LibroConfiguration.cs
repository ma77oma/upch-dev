using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using UPCH.Bookstore.Domain.Entities;
using UPCH.Bookstore.Infrastructure.Data.Base;

namespace UPCH.Bookstore.Infrastructure.Data.Configurations
{
    public class LibroConfiguration : EntityConfiguration<LibroEntity>
    {
        public LibroConfiguration(ModelBuilder modelBuilder)
        {
            var entityBuilder = modelBuilder.Entity<LibroEntity>();

            entityBuilder.ToTable("Libro");
            entityBuilder.HasKey(c => c.Id);

            entityBuilder.Property(c => c.ISBN).HasColumnName("ISBN").HasMaxLength(13).IsRequired();
            entityBuilder.Property(c => c.Titulo).HasColumnName("Titulo").HasMaxLength(255).IsRequired();
            entityBuilder.Property(c => c.AnioPublicacion).HasColumnName("AnioPublicacion").IsRequired();
            entityBuilder.Property(c => c.CantidadPaginas).HasColumnName("CantidadPaginas");

            entityBuilder.Property(c => c.EditorialId).HasColumnName("EditorialId");
            entityBuilder.Property(c => c.CategoriaId).HasColumnName("CategoriaId");

            entityBuilder.HasOne(l => l.Editorial)
                         .WithMany(e => e.Libros)
                         .HasForeignKey(l => l.EditorialId)
                         .HasConstraintName("FK_Libros_Editoriales");

            entityBuilder.HasOne(l => l.Categoria)
                         .WithMany(c => c.Libros)
                         .HasForeignKey(l => l.CategoriaId)
                         .HasConstraintName("FK_Libros_Categorias");

            Configure(entityBuilder);
        }
    }
}
