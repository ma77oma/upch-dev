using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPCH.Bookstore.Domain.Entities
{
    public class AutorEntity:BaseEntity
    {
        public string? Nombre { get; set; }
        public virtual ICollection<LibroAutorEntity> LibroAutor { get; set; } = [];
    }
}
