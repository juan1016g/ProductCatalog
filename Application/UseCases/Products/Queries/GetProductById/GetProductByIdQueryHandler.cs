using Application.DTOs;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            // 1. Obtener el producto desde el repositorio de infraestructura
            var product = await _productRepository.GetByIdAsync(request.Id) 
                ?? throw new KeyNotFoundException($"No se encontró un producto con el ID {request.Id}");

            // 2. Mapear y retornar la respuesta estructurada
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock
            };
        }
    }
}
