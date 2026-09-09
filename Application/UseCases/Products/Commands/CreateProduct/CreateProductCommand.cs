using Application.DTOs;
using MediatR;

namespace Application.UseCases.Products.Commands.CreateProduct
{
    /// <summary>
    /// Comando para crear un nuevo producto.
    /// </summary>
    public class CreateProductCommand : IRequest<ProductDto>
    {
        /// <summary>
        /// El nombre comercial del producto.
        /// </summary>
        public required string Name { get; set; }
        /// <summary>
        /// La descripción comercial del producto.
        /// </summary>
        public required string Description { get; set; }
        /// <summary>
        /// Precio unitario de venta. Debe ser mayor a 0.
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Cantidad inicial de stock.
        /// </summary>
        public int InitialStock { get; set; }
    }
}
