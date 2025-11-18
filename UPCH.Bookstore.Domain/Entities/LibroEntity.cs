using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPCH.Bookstore.Domain.Entities
{
    public class LibroEntity: BaseEntity
    {
        public string ISBN { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public int AnioPublicacion { get; set; }
        public int? CantidadPaginas { get; set; }
        public int? EditorialId { get; set; }
        public int? CategoriaId { get; set; }
        public virtual EditorialEntity? Editorial { get; set; }
        public virtual CategoriaEntity? Categoria { get; set; }
        public virtual ICollection<LibroAutorEntity> LibroAutor { get; set; } = [];
    }
}
