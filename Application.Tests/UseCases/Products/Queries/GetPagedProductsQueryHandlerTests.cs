using Application.UseCases.Products.Queries.GetPagedProducts;
using Domain.Entities;
using Domain.Repositories;
using Moq;

namespace Application.Tests.UseCases.Products.Queries
{
    public class GetPagedProductsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnPagedProducts_WhenProductsExist()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();

            var expectedProducts = new List<Product>
            {
                new("Producto A", 1000, 10),
                new("Producto B", 2000, 5, "Descripcion para el producto B"),
                new("Producto C", 5000, 0, "")
            };

            mockRepository.Setup(repo => repo.GetPagedAsync(1, 10)).ReturnsAsync(expectedProducts);
            mockRepository.Setup(repo => repo.GetTotalCountAsync()).ReturnsAsync(2);

            var handler = new GetPagedProductsQueryHandler(mockRepository.Object);
            var query = new GetPagedProductsQuery(1, 10);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.TotalRecords);
            Assert.Equal(3, result.Data.Count());

            mockRepository.Verify(repo => repo.GetPagedAsync(1, 10), Times.Once);
        }
    }
}
