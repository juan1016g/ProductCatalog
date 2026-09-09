using MediatR;

namespace Application.UseCases.Products.Commands.DeleteProduct
{
    /// <summary>
    /// Comando para eliminar un producto del catálogo.
    /// </summary>
    public class DeleteProductCommand : IRequest
    {
        /// <summary>
        /// El identificador único (GUID) del producto que se va a eliminar.
        /// </summary>
        public Guid Id { get; set; }
    }
}
