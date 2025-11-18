using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPCH.Bookstore.Infrastructure.Data.Base
{
    public abstract class EntityConfiguration<T> : IEntityConfiguration<T> where T : class
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property("Id").HasColumnName("Id").UseIdentityColumn();
            builder.Property("UsuarioCreacion").HasColumnName("UsuarioCreacion");
            builder.Property("FechaCreacion").HasColumnName("FechaCreacion");
            builder.Property("UsuarioModificacion").HasColumnName("UsuarioModificacion");
            builder.Property("FechaModificacion").HasColumnName("FechaModificacion");
        }
    }
}
