using MediatR;
using UPCH.Bookstore.Application.Common.Models;
using UPCH.Bookstore.Application.Libros.Commands.CreateLibro;
using UPCH.Bookstore.Application.Libros.DTOs;

namespace UPCH.Bookstore.Application.Libros.Commands.UpdateLibro
{
    public class UpdateLibroCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public int AnioPublicacion { get; set; }
        public int? CantidadPaginas { get; set; }
        public int? IdEditorial { get; set; }
        public int? IdCategoria { get; set; }
        public List<AutorCommand>? Autores { get; set; }
    }
}