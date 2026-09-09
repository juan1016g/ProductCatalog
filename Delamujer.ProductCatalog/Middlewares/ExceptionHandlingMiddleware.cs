using Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Delamujer.ProductCatalog.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió una excepción.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new ProblemDetails { Instance = context.Request.Path };

            switch (exception)
            {
                case ValidationException validationEx:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Title = "Error de validación";
                    response.Detail = "Uno o más campos tienen datos inválidos.";
                    response.Extensions["errors"] = validationEx.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                    break;
                case InvalidStockException stockEx:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Title = "Violación de regla de negocio";
                    response.Detail = stockEx.Message;
                    break;
                case KeyNotFoundException notFoundEx:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    response.Title = "Recurso no encontrado";
                    response.Detail = notFoundEx.Message;
                    break;
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    response.Title = "Error interno del servidor";
                    response.Detail = "Ocurrió un error inesperado.";
                    break;
            }

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
