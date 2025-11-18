using MediatR;
using UPCH.Bookstore.Application.Common.Models;
using UPCH.Bookstore.Application.Libros.DTOs;
using UPCH.Bookstore.Domain.Entities;
using UPCH.Bookstore.Infrastructure.Repositories.Interfaces;

namespace UPCH.Bookstore.Application.Libros.Commands.CreateLibro
{
    public class CreateLibroCommandHandler(ILibrosRepository repository) : IRequestHandler<CreateLibroCommand, Result<int>>
    {
        private readonly ILibrosRepository _repository = repository;

        public async Task<Result<int>> Handle(CreateLibroCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIsbnAsync(request.ISBN);

            if (entity != null)
                return Result<int>.Failure($"Libro con ISBN {request.ISBN} encontrado.");

            var param = request;

            var entidad = new LibroEntity
            {
                Titulo = param.Titulo,
                ISBN = param.ISBN ?? new Guid().ToString(),
                AnioPublicacion = param.AnioPublicacion,
                CantidadPaginas = param.CantidadPaginas,
                EditorialId = param.IdEditorial,
                CategoriaId = param.IdCategoria,
                UsuarioCreacion = "system",
                FechaCreacion = DateTime.Now
            };

            // 2. Mapear la relación Muchos a Muchos (LibroAutor)
            if (param.Autores != null)
            {
                foreach (var autorCmd in param.Autores)
                {
                    entidad.LibroAutor.Add(new LibroAutorEntity
                    {
                        AutorId = autorCmd.IdAutor,
                        Posicion = autorCmd.Posicion,
                        UsuarioCreacion = "system",
                        FechaCreacion = DateTime.Now
                    });
                }
            }

            var created = await _repository.AddAsync(entidad, cancellationToken);

            // Asumo que el Id devuelto es IdLibro (clave primaria)
            return Result<int>.Success(created.Id);
        }
    }
}