using Domain.Exceptions;

namespace Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }

        private Product() {
            Name = null!;
            //Description = null!;
        }

        public Product(string name, decimal price, int initialStock, string? description = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre del producto es obligatorio.");

            if (price <= 0)
                throw new ArgumentException("El precio debe ser mayor a cero.");

            if (initialStock < 0)
                throw new ArgumentException("El stock inicial no puede ser negativo.");

            Id = Guid.NewGuid();
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
            Price = price;
            Stock = initialStock;
        }

        public void UpdateStock(int quantity)
        {
            int newStock = Stock + quantity;

            if (newStock < 0) throw new InvalidStockException($"Operación inválida: El stock actual es {Stock}. No se puede restar {Math.Abs(quantity)} unidades.");
            
            Stock = newStock;
        }
    }
}
