using Application.DTOs;
using Application.UseCases.Products.Commands.CreateProduct;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Products.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // 1. Instanciar la entidad del dominio (ejecuta las validaciones del constructor)
            var product = new Product(
                request.Name,
                request.Price,
                request.InitialStock,
                request.Description);

            // 2. Persistir utilizando el contrato de Infraestructura
            await _productRepository.AddAsync(product);

            // 3. Mapear y retornar la respuesta estructurada
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
