using MediatR;
using UPCH.Bookstore.Application.Common.Models;

namespace UPCH.Bookstore.Application.Libros.Commands.DeleteLibro
{
    public class DeleteLibroCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
