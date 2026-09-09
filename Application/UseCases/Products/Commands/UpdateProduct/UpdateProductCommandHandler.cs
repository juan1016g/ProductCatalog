using Application.DTOs;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            // 1. Obtener el producto desde el repositorio de infraestructura
            var product = await _productRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"El producto con ID {request.Id} no fue encontrado.");

            // 2. Actualizar únicamente los campos permitidos (El Id y el Stock quedan intactos)
            product.UpdateInformation(request.Name, request.Description, request.Price);

            // 3. Persistir los cambios
            await _productRepository.UpdateAsync(product);

            return Unit.Value;
        }
    }
}
