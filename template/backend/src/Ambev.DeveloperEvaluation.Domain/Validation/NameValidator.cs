using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class NameValidator : AbstractValidator<Name>
    {
        /// <summary>
        /// Initializes a new instance of the NameValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - FirstName: Must be not null and not empty
        /// - LastName: Must be not null and not empty        
        /// </remarks>
        public NameValidator()
        {
            RuleFor(name => name.Firstname).NotEmpty().NotNull();
            RuleFor(name => name.Lastname).NotEmpty().NotNull();
        }
    }
}
