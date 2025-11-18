using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPCH.Bookstore.Application.Common.Models;

namespace UPCH.Bookstore.Application.Libros.Commands.CreateLibro
{
    public class CreateLibroCommand : IRequest<Result<int>>
    {
        public string ISBN { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public int AnioPublicacion { get; set; }
        public int? CantidadPaginas { get; set; }
        public int? IdEditorial { get; set; }
        public int? IdCategoria { get; set; }
        public List<AutorCommand> Autores { get; set; } = new List<AutorCommand>();
    }

    public class AutorCommand
    {
        public int IdAutor { get; set; }
        public int Posicion { get; set; } // 1 = Principal, 2 = Coautor, etc.
    }
}