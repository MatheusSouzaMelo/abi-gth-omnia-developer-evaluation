﻿using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser
{
    /// <summary>
    /// API request model for update operation
    /// </summary>
    public class UpdateUserRequest
    {
        /// <summary>
        /// Gets or sets the username. Must be unique and contain only valid characters.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password. Must meet security requirements.
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
        /// The user's email address
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The user's phone number
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// The user's role in the system
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        /// The current status of the user
        /// </summary>
        public UserStatus Status { get; set; }

    }
}
