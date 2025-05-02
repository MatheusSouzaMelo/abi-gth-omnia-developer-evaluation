using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    internal class GetSaleCommandValidator : AbstractValidator<GetSaleCommand>  
    {
        public GetSaleCommandValidator()
        {
            RuleFor(s => s.SaleId).NotEmpty().WithMessage("Sale Id can not be empty");
        }
    }
}
