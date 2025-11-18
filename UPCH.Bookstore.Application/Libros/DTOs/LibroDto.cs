using System;

namespace UPCH.Bookstore.Application.Libros.DTOs
{
    public class LibroDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string? ISBN { get; set; }
        public int AnioPublicacion { get; set; }
        public string? Categoria { get; set; }
        public string? Editorial { get; set; }
        public List<AutorDto>? Autores { get; set; }

    }

    public class AutorDto
    {

        public string? Nombre { get; set; }
        public string? Rol { get; set; }
    }
}
