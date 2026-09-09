using Domain.Repositories;
using FluentValidation;

namespace Application.UseCases.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("El precio unitario de venta debe ser mayor a 0.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("El nombre del producto es obligatorio.")
                .MustAsync(BeUniqueName)
                .WithMessage("Ya existe otro producto registrado con este nombre.");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("La descripción no puede exceder los 500 caracteres.");
        }

        private async Task<bool> BeUniqueName(UpdateProductCommand command, string name, CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository.GetByNameAsync(name);

            return (bool)(existingProduct is null || existingProduct.Id == command.Id);
        }
    }
}
