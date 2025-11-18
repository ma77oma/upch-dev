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
    public class CategoriaConfiguration : EntityConfiguration<CategoriaEntity>
    {
        public CategoriaConfiguration(ModelBuilder modelBuilder)
        {
            var entityBuilder = modelBuilder.Entity<CategoriaEntity>();

            entityBuilder.ToTable("Categoria");

            // Clave primaria
            entityBuilder.HasKey(c => c.Id);

            // Mapeo de columnas
            entityBuilder.Property(c => c.Nombre).HasColumnName("Nombre").HasMaxLength(100).IsRequired();

            // Índice Único para el nombre de la categoría
            entityBuilder.HasIndex(c => c.Nombre).IsUnique();

            Configure(entityBuilder);
        }
    }
}
