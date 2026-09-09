
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Products.Commands.UpdateStock
{
    public class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand, int>
    {
        private readonly IProductRepository _productRepository;

        public UpdateStockCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<int> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId) 
                ?? throw new KeyNotFoundException($"No se encontró un producto con el ID {request.ProductId}");

            product.UpdateStock(request.Quantity);

            await _productRepository.UpdateAsync(product);

            return product.Stock;
        }
    }
}
