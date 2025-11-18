using MediatR;
using UPCH.Bookstore.Application.Common.Models;
using UPCH.Bookstore.Application.Libros.DTOs;

namespace UPCH.Bookstore.Application.Libros.Queries.GetLibroById
{
    public class GetLibroByIdQuery : IRequest<Result<LibroDto>>
    {
        public int Id { get; set; }
    }
}