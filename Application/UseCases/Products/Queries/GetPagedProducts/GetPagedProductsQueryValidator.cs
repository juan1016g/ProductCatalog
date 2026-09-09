using FluentValidation;

namespace Application.UseCases.Products.Queries.GetPagedProducts
{
    public class GetPagedProductsQueryValidator : AbstractValidator<GetPagedProductsQuery>
    {
        public GetPagedProductsQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0)
                .WithMessage("El número de página debe ser mayor a 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage("El tamaño de la página debe ser mayor a 0.")
                .LessThanOrEqualTo(100)
                .WithMessage("El tamaño máximo de página permitido es 100.");
        }
    }
}
