using Domain.Repositories;
using FluentValidation;

namespace Application.UseCases.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(150).WithMessage("El nombre no puede exceder los 150 caracteres.")
                .MustAsync(BeUniqueName).WithMessage("Ya existe un producto registrado con este nombre.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(150).WithMessage("El nombre no puede exceder los 150 caracteres.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("La descripción es obligatoria.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("El precio unitario debe ser estrictamente mayor a cero.");

            RuleFor(x => x.InitialStock)
                .GreaterThanOrEqualTo(0).WithMessage("El stock inicial no puede ser negativo.");
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            // Devuelve true si es único (no existe), false si ya existe
            return await _productRepository.IsNameUniqueAsync(name);
        }

    }
}
