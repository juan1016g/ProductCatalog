using Application.DTOs;
using Application.UseCases.Products.Commands.CreateProduct;
using Application.UseCases.Products.Commands.UpdateStock;
using Application.UseCases.Products.Queries.GetPagedProducts;
using Application.UseCases.Products.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Delamujer.ProductCatalog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResultDto<ProductDto>>> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetPagedProductsQuery(page, pageSize));
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPatch("{id:guid}/stock")]
        public async Task<ActionResult<ProductDto>> UpdateStock(Guid id, [FromBody] UpdateStockCommand command)
        {
            command.ProductId = id;
            var newStock = await _mediator.Send(command);

            var result = await _mediator.Send(new GetProductByIdQuery(id));

            return CreatedAtAction(nameof(GetById), new { id = command.ProductId }, result);
        }
    }
}
