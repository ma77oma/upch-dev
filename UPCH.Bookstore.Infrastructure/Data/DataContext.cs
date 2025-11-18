using Microsoft.EntityFrameworkCore;
using UPCH.Bookstore.Domain.Entities;
using UPCH.Bookstore.Infrastructure.Configurations;

namespace UPCH.Bookstore.Infrastructure.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {

        // 1. Entidad Principal
        public DbSet<LibroEntity> Libro { get; set; } = null!; // Ya estaba

        // 2. Nuevas Entidades
        public DbSet<AutorEntity> Autor { get; set; } = null!;
        public DbSet<EditorialEntity> Editorial { get; set; } = null!;
        public DbSet<CategoriaEntity> Categoria { get; set; } = null!;

        // 3. Tabla de Relación Muchos a Muchos
        public DbSet<LibroAutorEntity> LibroAutor { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);

            base.OnModelCreating(builder);
        }
    }
}