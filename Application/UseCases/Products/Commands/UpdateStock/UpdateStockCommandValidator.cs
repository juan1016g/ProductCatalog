using FluentValidation;

namespace Application.UseCases.Products.Commands.UpdateStock
{
    public class UpdateStockCommandValidator : AbstractValidator<UpdateStockCommand>
    {
        public UpdateStockCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("El identificador del producto es obligatorio.");

            RuleFor(x => x.Quantity)
                .NotEqual(0).WithMessage("La cantidad a ajustar no puede ser cero. Debe ser un valor positivo o negativo.");
        }
    }
}
