using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Title).NotEmpty().NotNull();
            RuleFor(p => p.Price).GreaterThan(0).NotEmpty();
            RuleFor(p => p.Description).NotEmpty().NotNull();
            RuleFor(p => p.Category).NotEmpty().NotNull();
            RuleFor(p => p.Image).NotEmpty().NotNull();
        }
    }
}
