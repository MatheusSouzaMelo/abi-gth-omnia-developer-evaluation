using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CartProduct
{
    public class CartProductRequestValidator : AbstractValidator<CartProductRequest>
    {
        public CartProductRequestValidator()
        {
            RuleFor(p => p.ProductId).NotEmpty().WithMessage("Product is can not be empty");
            RuleFor(p => p.Quantity).GreaterThan(0).LessThanOrEqualTo(20).WithMessage("Product quantity should be between 1 and 20");
        }
    }
}
