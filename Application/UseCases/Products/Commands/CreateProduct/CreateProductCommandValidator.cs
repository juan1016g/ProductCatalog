using FluentValidation;

namespace Application.UseCases.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
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
    }
}
