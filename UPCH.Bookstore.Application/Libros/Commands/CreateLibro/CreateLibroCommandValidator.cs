using FluentValidation;

namespace UPCH.Bookstore.Application.Libros.Commands.CreateLibro
{
    using FluentValidation;

    public class CreateLibroCommandValidator : AbstractValidator<CreateLibroCommand>
    {
        public CreateLibroCommandValidator()
        {
            // Propiedades directas del Libro
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("El Título es obligatorio.")
                .MaximumLength(255).WithMessage("El Título no puede exceder los 255 caracteres.");

            RuleFor(x => x.ISBN)
                .NotEmpty().WithMessage("El ISBN es obligatorio.")
                .MaximumLength(13).WithMessage("El ISBN debe tener 13 caracteres."); // Asumiendo que es EAN-13

            RuleFor(x => x.AnioPublicacion)
                .GreaterThan(0).WithMessage("El Año de Publicación debe ser mayor a cero.")
                .LessThanOrEqualTo(DateTime.Now.Year).WithMessage($"El Año de Publicación no puede ser posterior a {DateTime.Now.Year}.");

            RuleFor(x => x.CantidadPaginas)
                .GreaterThan(0).When(x => x.CantidadPaginas.HasValue).WithMessage("La Cantidad de Páginas debe ser mayor a cero.");

            // Claves Foráneas (IDs)
            RuleFor(x => x.IdEditorial)
                .NotNull().WithMessage("Debe seleccionar una Editorial.")
                .GreaterThan(0).WithMessage("El ID de la Editorial debe ser válido.");

            RuleFor(x => x.IdCategoria)
                .NotNull().WithMessage("Debe seleccionar una Categoría.")
                .GreaterThan(0).WithMessage("El ID de la Categoría debe ser válido.");

            // Relación Muchos a Muchos (Autores)
            RuleFor(x => x.Autores)
                .NotEmpty().WithMessage("La lista de Autores no puede estar vacía. El libro debe tener al menos un autor.");

            RuleForEach(x => x.Autores).ChildRules(authors =>
            {
                authors.RuleFor(a => a.IdAutor)
                    .GreaterThan(0).WithMessage("El ID del Autor debe ser válido.");

                authors.RuleFor(a => a.Posicion)
                    .GreaterThan(0).WithMessage("La Posición del Autor debe ser mayor a cero.");
            });
        }
    }
}
