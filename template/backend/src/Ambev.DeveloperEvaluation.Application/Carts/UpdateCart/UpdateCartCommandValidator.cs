using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    internal class UpdateCartCommandValidator : AbstractValidator<UpdateCartCommand>
    {
        public UpdateCartCommandValidator()
        {
            RuleFor(cart => cart.Id).NotEmpty();
            RuleFor(cart => cart.Date).NotEmpty();
            RuleFor(cart => cart.Products).NotEmpty();
        }
    }
}
