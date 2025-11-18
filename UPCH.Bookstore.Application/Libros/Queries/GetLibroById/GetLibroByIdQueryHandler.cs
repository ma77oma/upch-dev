using MediatR;
using UPCH.Bookstore.Application.Common.Models;
using UPCH.Bookstore.Application.Libros.DTOs;
using UPCH.Bookstore.Infrastructure.Repositories.Interfaces;

namespace UPCH.Bookstore.Application.Libros.Queries.GetLibroById
{
    public class GetLibroByIdQueryHandler(ILibrosRepository repository) : IRequestHandler<GetLibroByIdQuery, Result<LibroDto>>
    {
        private readonly ILibrosRepository _repository = repository;

        public async Task<Result<LibroDto>> Handle(GetLibroByIdQuery request, CancellationToken cancellationToken)
        {
            var ent = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (ent == null)
                return Result<LibroDto>.Failure($"Libro con Id {request.Id} no encontrado.");

            var dto = new LibroDto
            {
                Id = ent.Id,
                Titulo = ent.Titulo,
                ISBN = ent.ISBN,
                AnioPublicacion = ent.AnioPublicacion,
                Categoria = ent.Categoria?.Nombre ?? "Categoría Desconocida",
                Editorial = ent.Editorial?.Nombre ?? "Editorial Desconocida",
                Autores = ent.LibroAutor?.Select(x => new AutorDto
                {
                    Nombre = x.Autor?.Nombre ?? "Autor Desconocido",
                    Rol = x.Posicion == 1 ? "Autor" : "Coautor"
                }).ToList()
            };

            return Result<LibroDto>.Success(dto);
        }
    }
}
