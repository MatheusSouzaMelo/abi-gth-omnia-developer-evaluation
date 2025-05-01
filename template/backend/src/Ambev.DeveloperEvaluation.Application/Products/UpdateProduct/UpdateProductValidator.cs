using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(p => p.Id).NotEmpty().WithMessage("Id can not be empty");
            RuleFor(p => p.Title).NotEmpty().NotNull().WithMessage("Title can not be null or empty");
            RuleFor(p => p.Price).GreaterThan(0).NotEmpty().WithMessage("Price must be grather than 0");
            RuleFor(p => p.Description).NotEmpty().NotNull().WithMessage("Description can not be null or empty");
            RuleFor(p => p.Category).NotEmpty().NotNull().WithMessage("Category can not be null or empty");
            RuleFor(p => p.Image).NotEmpty().NotNull().WithMessage("Image can not be null or empty");
        }
    }
}
