using MediatR;

namespace Application.UseCases.Products.Commands.UpdateStock
{
    /// <summary>
    /// Comando para actualizar el inventario de un producto específico.
    /// Retorna el nuevo nivel de inventario total tras la operación.
    /// </summary>
    public class UpdateStockCommand : IRequest<int>
    {
        /// <summary>
        /// El identificador único (GUID) del producto cuyo stock se va a modificar.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// La cantidad a ajustar en el inventario. 
        /// Utiliza valores positivos para incrementar el stock (ej. nueva mercancía) 
        /// y valores negativos para disminuirlo (ej. registro de ventas o mermas).
        /// </summary>
        public int Quantity { get; set; }
    }
}