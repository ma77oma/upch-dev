using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UPCH.Bookstore.Application.Common.Models;
using UPCH.Bookstore.Application.Libros.DTOs;
using UPCH.Bookstore.Infrastructure.Repositories.Interfaces;

namespace UPCH.Bookstore.Application.Libros.Queries.GetAllLibros
{
    public class GetAllLibrosQueryHandler : IRequestHandler<GetAllLibrosQuery, Result<List<LibroDto>>>
    {
        private readonly ILibrosRepository _repository;

        public GetAllLibrosQueryHandler(ILibrosRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<LibroDto>>> Handle(GetAllLibrosQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetAllAsync(cancellationToken);

            var dtos = list.Select(ent => new LibroDto
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
            }).ToList();

            return Result<List<LibroDto>>.Success(dtos);
        }
    }
}
