using Application.UseCases.Products.Commands.UpdateStock;
using Domain.Entities;
using Domain.Repositories;
using Moq;

namespace Application.Tests.UseCases.Products.Commands
{
    public class UpdateStockCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldUpdateStock_WhenProductExists()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var handler = new UpdateStockCommandHandler(mockRepository.Object);

            var productId = Guid.NewGuid();
            var command = new UpdateStockCommand 
            {
                ProductId = productId,
                Quantity = 5
            };

            // Creamos un producto falso simulando que ya existe en la base de datos con stock inicial de 10
            var existingProduct = new Product(
                "Teclado de prueba",
                150000m,
                10,
                "Descripción de prueba"
            );

            // Usamos Reflection para inyectar el Id privado, ya que el setter es privado (private set)
            typeof(Product).GetProperty("Id")?.SetValue(existingProduct, productId, null);

            // Configuramos el mock para que GetByIdAsync devuelva nuestro producto falso
            mockRepository.Setup(repo => repo.GetByIdAsync(productId))
                          .ReturnsAsync(existingProduct);

            mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Product>()))
                          .Returns(Task.CompletedTask);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(15, result);

            mockRepository.Verify(
                        repo => repo.UpdateAsync(It.Is<Product>(p => p.Stock == 15)),
                        Times.Once
                    );
        }
    }
}
