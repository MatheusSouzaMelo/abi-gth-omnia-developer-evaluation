using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSaleProducts
{
    public class UpdateSaleProductsCommandValidator : AbstractValidator<UpdateSaleProductsCommand>
    {
        public UpdateSaleProductsCommandValidator()
        {
            RuleFor(x => x.SaleId)
               .NotEmpty().WithMessage("Sale ID is required")
               .NotEqual(Guid.Empty).WithMessage("Invalid Sale ID");

            RuleForEach(x => x.Itens)
                .ChildRules(item =>
                {
                    item.RuleFor(i => i.ProductId)
                        .NotEmpty().WithMessage("Product ID is required")
                        .NotEqual(Guid.Empty).WithMessage("Invalid Product ID");

                    item.RuleFor(i => i.Quantity)
                        .ExclusiveBetween(0, 20).WithMessage("Quantity must be positive greather than 0 and lesser than 20");

                });
        }
    }
}
