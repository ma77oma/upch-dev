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
    public class AutorConfiguration : EntityConfiguration<AutorEntity>
    {
        public AutorConfiguration(ModelBuilder modelBuilder)
        {
            var entityBuilder = modelBuilder.Entity<AutorEntity>();

            entityBuilder.ToTable("Autor");

            entityBuilder.HasKey(c => c.Id);

            entityBuilder.Property(c => c.Nombre).HasColumnName("Nombre").HasMaxLength(255).IsRequired();

            Configure(entityBuilder);
        }
    }
}
