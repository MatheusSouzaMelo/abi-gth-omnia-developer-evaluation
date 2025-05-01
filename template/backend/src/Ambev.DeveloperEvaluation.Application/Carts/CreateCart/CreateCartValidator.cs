using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

public class CreateCartCommandValidator : AbstractValidator<CreateCartCommand>
{
    public CreateCartCommandValidator()
    {
        RuleFor(cart => cart.UserId).NotEmpty().WithMessage("User Id can not be empty");
        RuleFor(cart => cart.Date).NotEmpty().WithMessage("Cart date can not be empty");
        RuleForEach(cart => cart.Products).NotNull().NotEmpty();
    }
}