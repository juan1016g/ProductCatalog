using MediatR;
using System.Text.Json.Serialization;

namespace Application.UseCases.Products.Commands.UpdateProduct
{
    /// <summary>
    /// Comando para actualizar la información general de un producto.
    /// </summary>
    public class UpdateProductCommand : IRequest<Unit>
    {
        /// <summary>
        /// El identificador único (GUID) del producto a actualizar.
        /// Se ignora en el JSON de entrada porque se captura directamente desde la URL.
        /// </summary>
        [JsonIgnore]
        public Guid Id { get; set; }

        /// <summary>
        /// El nombre del producto.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// La descripción del producto.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// El precio del producto.
        /// </summary>
        public decimal Price { get; set; }
    }
}