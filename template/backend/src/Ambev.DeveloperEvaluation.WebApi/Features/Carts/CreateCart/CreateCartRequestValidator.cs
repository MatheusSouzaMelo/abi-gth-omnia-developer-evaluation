using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CartProduct;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart
{
    public class CreateCartRequestValidator : AbstractValidator<CreateCartRequest>
    {
        public CreateCartRequestValidator()
        {
            RuleFor(cart => cart.UserId).NotEmpty();
            RuleFor(cart => cart.Date).NotEmpty();
            RuleForEach(cart => cart.Products).SetValidator(new CartProductRequestValidator()).When(p => p.Products != null);
        }
    }
}
