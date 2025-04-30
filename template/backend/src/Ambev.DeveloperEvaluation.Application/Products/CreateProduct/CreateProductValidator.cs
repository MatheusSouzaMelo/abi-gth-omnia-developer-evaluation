using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Title).NotEmpty().NotNull();
            RuleFor(p => p.Price).GreaterThan(0).NotEmpty();
            RuleFor(p => p.Description).NotEmpty().NotNull();
            RuleFor(p => p.Category).NotEmpty().NotNull();
            RuleFor(p => p.Image).NotEmpty().NotNull();
        }
    }
}
