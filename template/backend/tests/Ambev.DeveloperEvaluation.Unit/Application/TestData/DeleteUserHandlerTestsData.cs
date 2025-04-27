using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users
{
    /// <summary>
    /// Provides methods for generating test data using the Bogus library.
    /// This class centralizes all test data generation to ensure consistency
    /// across test cases and provide both valid and invalid data scenarios.
    /// </summary>
    public static class DeleteUserHandlerTestsData
    {
        /// <summary>
        /// Configures the Faker to generate valid DeleteUserCommand entities.
        /// The generated DeleteUserCommand will have valid:
        /// - Id (valid format)
        /// </summary>
        public static readonly Faker<DeleteUserCommand> deleteUserCommandFaker = new Faker<DeleteUserCommand>()
            .CustomInstantiator(f => new DeleteUserCommand(id: Guid.NewGuid()));
        
        /// <summary>
        /// Configures the Faker to generate invalid DeleteUserCommand entities.
        /// The generated DeleteUserCommand will have invalid:
        /// - Id (Guid Empty)
        /// </summary>
        public static readonly Faker<DeleteUserCommand> deleteUserInvalidCommandFaker = new Faker<DeleteUserCommand>()
            .CustomInstantiator(f => new DeleteUserCommand(id: Guid.Empty));

        public static DeleteUserCommand GenerateCommand()
        {
            return deleteUserCommandFaker.Generate();
        }


        public static DeleteUserCommand GenerateInvalidCommand()
        {
            return deleteUserInvalidCommandFaker.Generate();
        }
    }
}
