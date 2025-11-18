using System.Collections.Generic;
using MediatR;
using UPCH.Bookstore.Application.Common.Models;
using UPCH.Bookstore.Application.Libros.DTOs;

namespace UPCH.Bookstore.Application.Libros.Queries.GetAllLibros
{
    public class GetAllLibrosQuery : IRequest<Result<List<LibroDto>>>
    {
    }
}