using Application.UseCases.Products.Queries.GetProductById;
using Domain.Entities;
using Domain.Repositories;
using Moq;

namespace Application.Tests.UseCases.Products.Queries
{
    public class GetProductByIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnProductDto_WhenProductExists()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var handler = new GetProductByIdQueryHandler(mockRepository.Object);

            var productId = Guid.NewGuid();
            var query = new GetProductByIdQuery(productId);

            var existingProduct = new Product("Monitor 24 pulgadas", 500000m, 5, "Monitor IPS");
            typeof(Product).GetProperty("Id")?.SetValue(existingProduct, productId, null);

            mockRepository.Setup(repo => repo.GetByIdAsync(productId))
                          .ReturnsAsync(existingProduct);

            // Act
            var resultDto = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(resultDto);
            Assert.Equal(productId, resultDto.Id);
            Assert.Equal("Monitor 24 pulgadas", resultDto.Name);
        }

        [Fact]
        public async Task Handle_ShouldThrowKeyNotFoundException_WhenProductDoesNotExist()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var handler = new GetProductByIdQueryHandler(mockRepository.Object);
            var query = new GetProductByIdQuery(Guid.NewGuid());

            mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                          .ReturnsAsync((Product)null!);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => handler.Handle(query, CancellationToken.None));
        }
    }
}
