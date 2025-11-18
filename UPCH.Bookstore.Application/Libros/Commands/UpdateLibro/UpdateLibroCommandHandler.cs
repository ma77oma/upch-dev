using MediatR;
using UPCH.Bookstore.Application.Common.Models;
using UPCH.Bookstore.Domain.Entities;
using UPCH.Bookstore.Infrastructure.Repositories.Interfaces;

namespace UPCH.Bookstore.Application.Libros.Commands.UpdateLibro
{
    public class UpdateLibroCommandHandler(ILibrosRepository repository) : IRequestHandler<UpdateLibroCommand, Result>
    {
        private readonly ILibrosRepository _repository = repository;

        public async Task<Result> Handle(UpdateLibroCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (existing == null)
                return Result.Failure($"Libro con Id {request.Id} no encontrado.");

            existing.Titulo = request.Titulo;
            existing.AnioPublicacion = request.AnioPublicacion;
            existing.CantidadPaginas = request.CantidadPaginas;
            existing.CategoriaId = request.IdCategoria;
            existing.EditorialId = request.IdEditorial;

            existing.UsuarioModificacion = "system";
            existing.FechaModificacion = DateTime.Now;


            if (request.Autores != null)
            {
                existing.LibroAutor.Clear();

                foreach (var autorCmd in request.Autores)
                {
                    existing.LibroAutor.Add(new LibroAutorEntity
                    {
                        LibroId = request.Id,
                        AutorId = autorCmd.IdAutor,
                        Posicion = autorCmd.Posicion,
                        UsuarioCreacion = "system",
                        FechaCreacion = DateTime.Now
                    });
                }
            }

            await _repository.UpdateAsync(existing, cancellationToken);

            return Result<int>.Success(1);
        }
    }
}
