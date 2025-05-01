using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct
{
    internal class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductValidator()
        {
            RuleFor(p => p.Id).NotEmpty().WithMessage("Id can not be empty");
        }
    }
}
