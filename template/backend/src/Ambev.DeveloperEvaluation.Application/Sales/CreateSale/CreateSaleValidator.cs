using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleValidator()
        {
            RuleFor(x => x.CartId)
                .NotEmpty().WithMessage("Cart ID is required");

            RuleFor(x => x.Branch)
                .NotEmpty().WithMessage("Branch is required")
                .MaximumLength(100).WithMessage("Branch must not exceed 100 characters");
        }
    }
}
