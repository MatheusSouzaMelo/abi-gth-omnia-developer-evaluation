using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class AddressValidator : AbstractValidator<Address>
    {
        /// <summary>
        /// Initializes a new instance of the AddressValidator with defined validation rules.
        /// </summary>
        /// <remarks>
        /// Validation rules include:
        /// - City: Must be not null and not empty
        /// - Street Name: Must be not null and not empty        
        /// - Number: Must be greater than 0        
        /// - ZipCode: Must be not null and not empty        
        /// </remarks>
        public AddressValidator()
        {
            RuleFor(address => address.City).NotEmpty().NotNull();
            RuleFor(address => address.Street).NotEmpty().NotNull();
            RuleFor(address => address.Number).GreaterThan(0);
            RuleFor(address => address.Zipcode).NotEmpty().NotNull();
        }
    }
}
