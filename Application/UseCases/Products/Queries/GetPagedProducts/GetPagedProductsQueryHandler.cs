using Application.DTOs;
using Domain.Repositories;
using MediatR;

namespace Application.UseCases.Products.Queries.GetPagedProducts
{
    public class GetPagedProductsQueryHandler : IRequestHandler<GetPagedProductsQuery, PagedResultDto<ProductDto>>
    {
        private readonly IProductRepository _productRepository;

        public GetPagedProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<PagedResultDto<ProductDto>> Handle(GetPagedProductsQuery request, CancellationToken cancellationToken)
        {
            // 1. Obtener la cantidad total de registros para el cálculo matemático
            var totalRecords = await _productRepository.GetTotalCountAsync();

            // 2. Obtener solo la porción de datos correspondiente a la página solicitada
            var products = await _productRepository.GetPagedAsync(request.Page, request.PageSize);

            // 3.Mapear la lista de entidades puras a DTOs
            var dtoList = products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Stock = p.Stock
                }
            );

            // 4. Construir y retornar el envoltorio de paginación
            return new PagedResultDto<ProductDto>
            {
                Data = dtoList,
                CurrentPage = request.Page,
                PageSize = request.PageSize,
                TotalRecords = totalRecords
            };
        }
    }
}
