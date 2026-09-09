namespace Application.DTOs
{
    /// <summary>
    /// Objeto de transferencia de datos (DTO) que representa la información de un producto.
    /// Utilizado para las respuestas de la API.
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// El identificador único del producto.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// El nombre comercial del producto.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Descripción detallada del producto (Opcional).
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Precio unitario de venta del producto.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Cantidad actual de unidades disponibles en inventario.
        /// </summary>
        public int Stock { get; set; }
    }
}