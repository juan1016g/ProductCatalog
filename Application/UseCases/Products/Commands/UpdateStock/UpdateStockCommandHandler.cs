
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
            // 1. Obtener el producto desde el repositorio de infraestructura
            var product = await _productRepository.GetByIdAsync(request.ProductId) 
                ?? throw new KeyNotFoundException($"No se encontró un producto con el ID {request.ProductId}");

            // 2. Ejecutar la regla de negocio del núcleo (Lanza InvalidStockException si queda en negativo) [RN-01]
            product.UpdateStock(request.Quantity);

            // 3. Persistir el cambio atómicamente [RF-04]
            await _productRepository.UpdateAsync(product);

            // 4. Retornar el nuevo stock para la confirmación HTTP 200
            return product.Stock;
        }
    }
}
