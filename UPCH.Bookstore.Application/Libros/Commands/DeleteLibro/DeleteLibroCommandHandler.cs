using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UPCH.Bookstore.Application.Common.Models;
using UPCH.Bookstore.Infrastructure.Repositories.Interfaces;

namespace UPCH.Bookstore.Application.Libros.Commands.DeleteLibro
{
    public class DeleteLibroCommandHandler : IRequestHandler<DeleteLibroCommand, Result>
    {
        private readonly ILibrosRepository _repository;

        public DeleteLibroCommandHandler(ILibrosRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteLibroCommand request, CancellationToken cancellationToken)
        {
            var deleted = await _repository.DeleteByIdAsync(request.Id, cancellationToken);
            if (!deleted)
                return Result.Failure($"No se encontró el libro con Id {request.Id}.");

            return Result.Success();
        }
    }
}
