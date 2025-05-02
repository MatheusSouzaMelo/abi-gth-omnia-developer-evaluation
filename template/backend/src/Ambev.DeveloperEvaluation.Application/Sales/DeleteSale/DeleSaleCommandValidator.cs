using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    internal class DeleSaleCommandValidator : AbstractValidator<DeleteSaleCommand>
    {
        public DeleSaleCommandValidator()
        {
            RuleFor(s => s.SaleId).NotEmpty().WithMessage("Sale Id can not be empty");
        }
    }
}
