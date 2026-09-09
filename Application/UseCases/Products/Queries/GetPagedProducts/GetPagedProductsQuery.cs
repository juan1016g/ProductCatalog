using Application.DTOs;
using MediatR;

namespace Application.UseCases.Products.Queries.GetPagedProducts
{
    public class GetPagedProductsQuery : IRequest<PagedResultDto<ProductDto>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public GetPagedProductsQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }
}
