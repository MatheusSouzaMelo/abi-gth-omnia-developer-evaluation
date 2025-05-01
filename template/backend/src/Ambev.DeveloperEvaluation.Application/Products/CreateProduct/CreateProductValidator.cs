using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Title).NotEmpty().NotNull().WithMessage("Title can not be null or empty");
            RuleFor(p => p.Price).GreaterThan(0).NotEmpty().WithMessage("Price must be grather than 0");
            RuleFor(p => p.Description).NotEmpty().NotNull().WithMessage("Description can not be null or empty");
            RuleFor(p => p.Category).NotEmpty().NotNull().WithMessage("Category can not be null or empty");
            RuleFor(p => p.Image).NotEmpty().NotNull().WithMessage("Image can not be null or empty");
        }
    }
}
