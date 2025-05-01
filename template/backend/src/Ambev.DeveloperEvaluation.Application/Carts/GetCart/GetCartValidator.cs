using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart
{
    public class GetCartValidator : AbstractValidator<GetCartCommand>
    {
        public GetCartValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Cart ID is required"); ;
        }
    }
}
