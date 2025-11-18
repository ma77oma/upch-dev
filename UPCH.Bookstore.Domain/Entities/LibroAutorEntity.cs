using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPCH.Bookstore.Domain.Entities
{
    public class LibroAutorEntity : BaseEntity
    {
        public int LibroId { get; set; }
        public int AutorId { get; set; }
        public int Posicion { get; set; }
        public virtual LibroEntity Libro { get; set; } = null!;
        public virtual AutorEntity Autor { get; set; } = null!;
    }
}
