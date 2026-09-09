using Application.DTOs;
using MediatR;

namespace Application.UseCases.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<ProductDto>
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public int InitialStock { get; set; }
    }
}
