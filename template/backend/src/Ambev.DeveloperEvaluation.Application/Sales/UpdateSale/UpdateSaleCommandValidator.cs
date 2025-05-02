using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Sale Id is required");

            RuleFor(x => x.Branch)
                .NotEmpty().WithMessage("Branch is required")
                .MaximumLength(100).WithMessage("Branch must not exceed 100 characters");

            RuleFor(x => x.IsCancelled).NotEmpty();
        }
    }
}
