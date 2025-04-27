using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users
{
    /// <summary>
    /// Provides methods for generating test data using the Bogus library.
    /// This class centralizes all test data generation to ensure consistency
    /// across test cases and provide both valid and invalid data scenarios.
    /// </summary>
    public static class GetUserHandlerTestData
    {
        /// <summary>
        /// Configures the Faker to generate valid GetUserCommand entities.
        /// The generated GetUserCommand will have valid:
        /// - Id (valid format)
        /// </summary>
        public static readonly Faker<GetUserCommand> getUserCommandFaker = new Faker<GetUserCommand>()
            .CustomInstantiator(f => new GetUserCommand(id: Guid.NewGuid()));

        /// <summary>
        /// Configures the Faker to generate invalid GetUserCommand entities.
        /// The generated GetUserCommand will have invalid:
        /// - Id (Guid Empty)
        /// </summary>
        public static readonly Faker<GetUserCommand> getUserInvalidCommandFaker = new Faker<GetUserCommand>()
            .CustomInstantiator(f => new GetUserCommand(id: Guid.Empty));

        public static GetUserCommand GenerateCommand()
        {
            return getUserCommandFaker.Generate();
        }


        public static GetUserCommand GenerateInvalidCommand()
        {
            return getUserInvalidCommandFaker.Generate();
        }

    }
}
