using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users
{
    /// <summary>
    /// Provides methods for generating test data using the Bogus library.
    /// This class centralizes all test data generation to ensure consistency
    /// across test cases and provide both valid and invalid data scenarios.
    /// </summary>
    public static class AuthenticateUserHandlerTestData
    {
        /// <summary>
        /// Configures the Faker to generate valid AuthenticateUserCommand entities.
        /// The generated AuthenticateUserCommand will have valid:
        /// - Email (valid format)
        /// - Password (meeting complexity requirements)
        /// </summary>
        public static readonly Faker<AuthenticateUserCommand> authenticateUserCommandFaker = new Faker<AuthenticateUserCommand>()
            .RuleFor(f => f.Email, f => f.Internet.Email())
            .RuleFor(u => u.Password, f => $"Test@{f.Random.Number(100, 999)}");

        public static AuthenticateUserCommand GenerateCommand()
        {
            return authenticateUserCommandFaker.Generate();
        }
    }
}
