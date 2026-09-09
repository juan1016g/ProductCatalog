using Application.DTOs;
using Application.UseCases.Products.Commands.CreateProduct;
using Application.UseCases.Products.Commands.UpdateStock;
using Application.UseCases.Products.Queries.GetPagedProducts;
using Application.UseCases.Products.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Delamujer.ProductCatalog.Controllers
{
    /// <summary>
    /// Controlador principal para la gestión del catálogo y el inventario de productos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obtiene un producto específico utilizando su identificador único.
        /// </summary>
        /// <param name="id">El GUID del producto a consultar.</param>
        /// <returns>El detalle del producto solicitado.</returns>
        /// <response code="200">Retorna los datos del producto.</response>
        /// <response code="404">Si no existe ningún producto con el ID proporcionado.</response>
        /// <response code="500">Error interno del servidor, típicamente por problemas de conexión a la base de datos.</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(result);
        }

        /// <summary>
        /// Consulta el catálogo completo de productos con paginación desde el servidor.
        /// </summary>
        /// <param name="page">El número de la página a consultar (por defecto 1).</param>
        /// <param name="pageSize">La cantidad de registros por página (por defecto 10).</param>
        /// <returns>Una lista paginada de productos disponibles.</returns>
        /// <response code="200">Retorna la colección de productos y los metadatos de paginación.</response>
        /// <response code="400">Si los parámetros de paginación son inválidos (ej. página o tamaño menor a 1).</response>
        /// <response code="500">Error interno del servidor, típicamente por problemas de conexión a la base de datos (Ej. Azure SQL Server timeout).</response>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResultDto<ProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedResultDto<ProductDto>>> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _mediator.Send(new GetPagedProductsQuery(page, pageSize));
            return Ok(result);
        }

        /// <summary>
        /// Crea un nuevo producto en el catálogo.
        /// </summary>
        /// <remarks>
        /// Ejemplo de petición:
        ///
        ///     POST /api/products
        ///     {
        ///        "name": "Teclado Mecánico EPOMAKER",
        ///        "description": "Switch Red, distribución ISO español",
        ///        "price": 350000,
        ///        "initialStock": 15
        ///     }
        ///
        /// </remarks>
        /// <param name="command">Los datos requeridos para registrar el producto.</param>
        /// <returns>El detalle del producto recién creado.</returns>
        /// <response code="201">El producto se creó correctamente.</response>
        /// <response code="400">Si los datos enviados no cumplen con las reglas de validación (ej. precio negativo).</response>
        /// <response code="500">Error interno del servidor, típicamente por problemas de conexión a la base de datos.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Ajusta el inventario de un producto existente.
        /// </summary>
        /// <remarks>
        /// Para sumar stock (ej. nueva mercancía), envía un número positivo:
        /// 
        ///     PATCH /api/products/{id}/stock
        ///     {
        ///        "quantity": 10
        ///     }
        ///     
        /// Para restar stock (ej. una venta), envía un número negativo:
        /// 
        ///     PATCH /api/products/{id}/stock
        ///     {
        ///        "quantity": -3
        ///     }
        ///     
        /// </remarks>
        /// <param name="id">El GUID del producto a actualizar.</param>
        /// <param name="command">El comando que contiene la cantidad a ajustar.</param>
        /// <returns>El detalle del producto con el stock actualizado.</returns>
        /// <response code="201">El stock fue actualizado correctamente y retorna el producto.</response>
        /// <response code="400">Si la operación intenta dejar el inventario en números negativos.</response>
        /// <response code="404">Si el producto especificado no existe.</response>
        /// <response code="500">Error interno del servidor, típicamente por problemas de conexión a la base de datos.</response>
        [HttpPatch("{id:guid}/stock")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductDto>> UpdateStock(Guid id, [FromBody] UpdateStockCommand command)
        {
            command.ProductId = id;
            var newStock = await _mediator.Send(command);

            var result = await _mediator.Send(new GetProductByIdQuery(id));

            return CreatedAtAction(nameof(GetById), new { id = command.ProductId }, result);
        }
    }
}
