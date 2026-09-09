using Application.UseCases.Products.Commands;
using Application.UseCases.Products.Commands.CreateProduct;
using Domain.Repositories;
using Moq;
using Domain.Entities;

namespace Application.Tests.UseCases.Products.Commands
{
    public class CreateProductCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldCreateProduct_WhenRequestIsValid()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var handler = new CreateProductCommandHandler(mockRepository.Object);

            var command = new CreateProductCommand
            {
                Name = "Teclado Mecánico EPOMAKER Ajazz AK820 PRO",
                Description = "Teclado custom con keycaps OEM en español",
                Price = 350000.00m,
                InitialStock = 2
            };

            // Simulamos que el repositorio no hace nada al agregar (devuelve Task completado)
            mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Product>()))
                          .Returns(Task.CompletedTask);

            // Act
            var resultDto = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(resultDto);
            Assert.Equal("Teclado Mecánico EPOMAKER Ajazz AK820 PRO", resultDto.Name);
            Assert.Equal(350000.00m, resultDto.Price);

            mockRepository.Verify(
                repo => repo.AddAsync(It.Is<Product>(p =>
                    p.Name == "Teclado Mecánico EPOMAKER Ajazz AK820 PRO" &&
                    p.Price == 350000.00m)),
                Times.Once
            );
        }
    }
}
