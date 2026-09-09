using Domain.Entities;
using Domain.Exceptions;

namespace Domain.Tests.Entities
{
    public class ProductTests
    {
        [Fact]
        public void UpdateStock_ShouldIncreaseStock_WhenQuantityIsPositive()
        {
            // Arrange
            var product = new Product("Mouse Inalámbrico", 50000m, 10, "Mouse ergonómico");

            // Act
            product.UpdateStock(5);

            // Assert
            Assert.Equal(15, product.Stock);
        }

        [Fact]
        public void UpdateStock_ShouldDecreaseStock_WhenQuantityIsNegativeButSufficient()
        {
            // Arrange
            var product = new Product("Mouse Inalámbrico", 50000m, 10, "Mouse ergonómico");

            // Act
            product.UpdateStock(-3); // Restamos 3

            // Assert
            Assert.Equal(7, product.Stock);
        }

        [Fact]
        public void UpdateStock_ShouldThrowInvalidStockException_WhenStockDropsBelowZero()
        {
            // Arrange
            var product = new Product("Mouse Inalámbrico", 50000m, 10, "Mouse ergonómico");

            // Act & Assert
            var exception = Assert.Throws<InvalidStockException>(() => product.UpdateStock(-15));

            Assert.Contains("stock", exception.Message.ToLower());
        }

        [Fact]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            // Arrange & Act
            var product = new Product("Silla Gamer", 850000m, 2, "Silla ergonómica negra");

            // Assert
            Assert.Equal("Silla Gamer", product.Name);
            Assert.Equal(850000m, product.Price);
            Assert.Equal(2, product.Stock);
            Assert.Equal("Silla ergonómica negra", product.Description);

            Assert.Null(product.CreatedBy);
        }

        [Fact]
        public void PrivateConstructor_ShouldInitializeForEntityFramework()
        {
            // Arrange & Act
            var product = (Product)Activator.CreateInstance(typeof(Product), nonPublic: true)!;

            // Assert
            Assert.NotNull(product);
            Assert.Null(product.Name);
        }

    }
}
