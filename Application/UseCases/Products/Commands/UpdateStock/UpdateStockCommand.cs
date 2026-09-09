using MediatR;

namespace Application.UseCases.Products.Commands.UpdateStock
{
    public class UpdateStockCommand : IRequest<int>
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
