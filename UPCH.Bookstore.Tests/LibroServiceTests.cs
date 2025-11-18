using Moq;
using UPCH.Bookstore.Application.Libros.Queries.GetLibroById;
using UPCH.Bookstore.Domain.Entities;
using UPCH.Bookstore.Infrastructure.Repositories.Implementations;
using UPCH.Bookstore.Infrastructure.Repositories.Interfaces;
using Xunit;

public class ObtenerLibroPorIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_ObtenerLibroPorId_DebeRetornarLibro_SiExiste()
    {
        // 1. ARRANGE (Configuración de Mocks y Objetos)

        // Mock del Repositorio de Lectura/Query
        var mockRepo = new Mock<ILibrosRepository>();

        // Entidad de la BD que el Mock retornará
        var libroEntity = new LibroEntity { Id = 1, Titulo = "Cien Años de Soledad" };

        // Configuración de la Query de entrada
        var query = new GetLibroByIdQuery() { Id = 1 };

        // Simula la respuesta del repositorio para el ID 1
        mockRepo.Setup(repo => repo.GetByIdAsync(query.Id, It.IsAny<CancellationToken>())) // SOLUCIÓN
            .ReturnsAsync(libroEntity);

        // 2. Mock del Mapeador (Mapper) - Crucial en CQRS

        // Crea una instancia del Handler, inyectando el mock
        var handler = new GetLibroByIdQueryHandler(mockRepo.Object);

        var resultado = await handler.Handle(query, CancellationToken.None);

        // 4. ASSERT (Verificación)

        // Verifica que el resultado no sea nulo
        Assert.NotNull(resultado);

        // Verifica que el Handler llamó al repositorio exactamente una vez
        mockRepo.Verify(repo => repo.GetByIdAsync(query.Id, default), Times.Once);

        // Verifica que los datos son correctos
        Assert.Equal(libroEntity.Titulo, resultado!.Data!.Titulo);
        Assert.Equal(libroEntity.Id, resultado!.Data!.Id);
    }
}