using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart
{
    public class UpdateCartRequestValidator : AbstractValidator<UpdateCartRequest>
    {
        public UpdateCartRequestValidator()
        {
            RuleFor(cart => cart.Date).NotEmpty();
            RuleFor(cart => cart.Products).NotEmpty();
        }
    }
}
