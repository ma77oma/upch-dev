using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UPCH.Bookstore.Domain.Entities;

namespace UPCH.Bookstore.Infrastructure.Repositories.Interfaces
{
    public interface ILibrosRepository
    {
        Task<List<LibroEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<LibroEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<LibroEntity?> GetByIsbnAsync(string isbn, CancellationToken cancellationToken = default);
        Task<LibroEntity> AddAsync(LibroEntity libro, CancellationToken cancellationToken = default);
        Task UpdateAsync(LibroEntity libro, CancellationToken cancellationToken = default);
        Task<bool> DeleteByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
