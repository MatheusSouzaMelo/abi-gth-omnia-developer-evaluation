using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser
{
    public class UpdateUserCommand :IRequest<UpdateUserResult>
    {
        /// <summary>
        /// Gets or sets the user Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets the username of the user to be created.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password for the user.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name. Must be not null.
        /// </summary>
        public Name Name { get; set; } = new Name();

        /// <summary>
        /// Gets or sets the address. Must be not null.
        /// </summary>
        public Address Address { get; set; } = new Address();

        /// <summary>
        /// Gets or sets the phone number for the user.
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address for the user.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the status of the user.
        /// </summary>
        public UserStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the role of the user.
        /// </summary>
        public UserRole Role { get; set; }
    }
}
