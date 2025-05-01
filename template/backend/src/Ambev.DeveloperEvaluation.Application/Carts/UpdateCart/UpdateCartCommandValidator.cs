using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    internal class UpdateCartCommandValidator : AbstractValidator<UpdateCartCommand>
    {
        public UpdateCartCommandValidator()
        {
            RuleFor(cart => cart.UserId).NotEmpty().WithMessage("User Id can not be empty");
            RuleFor(cart => cart.Date).NotEmpty().WithMessage("Cart date can not be empty");
            RuleFor(cart => cart.Products).NotEmpty();
        }
    }
}
