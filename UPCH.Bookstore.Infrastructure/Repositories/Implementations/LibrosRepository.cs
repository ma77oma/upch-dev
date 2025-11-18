using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UPCH.Bookstore.Domain.Entities;
using UPCH.Bookstore.Infrastructure.Data;
using UPCH.Bookstore.Infrastructure.Repositories.Interfaces;

namespace UPCH.Bookstore.Infrastructure.Repositories.Implementations
{
    public class LibrosRepository(DataContext context) : ILibrosRepository
    {
        private readonly DataContext _context = context;

        // Obtener todos los libros
        public async Task<List<LibroEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Libro
                .Include(x => x.Categoria)
                .Include(x => x.Editorial)
                .Include(x => x.LibroAutor)
                 .ThenInclude(x => x.Autor)
                .AsNoTracking().ToListAsync(cancellationToken);
        }

        // Obtener uno por Id
        public async Task<LibroEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Libro
                    .Include(x => x.Categoria)
                .Include(x => x.Editorial)
                .Include(x => x.LibroAutor)
                 .ThenInclude(x => x.Autor)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        // Buscar por ISBN
        public async Task<LibroEntity?> GetByIsbnAsync(string isbn, CancellationToken cancellationToken = default)
        {
            return await _context.Libro
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.ISBN == isbn);
        }

        // Agregar
        public async Task<LibroEntity> AddAsync(LibroEntity libro, CancellationToken cancellationToken = default)
        {
            var entry = await _context.Libro.AddAsync(libro);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        // Actualizar (se espera entidad trackeada o attach)
        public async Task UpdateAsync(LibroEntity libro, CancellationToken cancellationToken = default)
        {
            _context.Libro.Update(libro);
            await _context.SaveChangesAsync();
        }

        // Eliminar por entidad
        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);

            if (entity != null)
            {
                _context.Libro.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        // Eliminar por id
        public async Task<bool> DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var libro = await _context.Libro.FindAsync(id);
            if (libro == null) return false;
            _context.Libro.Remove(libro);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
