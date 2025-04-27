using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Users;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users
{
    /// <summary>
    /// Contains unit tests for the <see cref="DeleteUserHandler"/> class.
    /// </summary>
    public class DeleteUserHandlerTests
    {
        private readonly IUserRepository _userRepository;
        private readonly DeleteUserHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteUserHandlerTests"/> class.
        /// Sets up the test dependencies and creates fake data generators.
        /// </summary>
        public DeleteUserHandlerTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new DeleteUserHandler(_userRepository);
        }

        /// <summary>
        /// Tests that a valid user deletion request is handled successfully.
        /// </summary>
        [Fact(DisplayName = "Given valid user data When deleting user Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            //Given
            var command = DeleteUserHandlerTestsData.GenerateCommand();
            _userRepository.DeleteAsync(Arg.Any<Guid>()).Returns(true);

            //When
            var result = await _handler.Handle(command, CancellationToken.None);

            //Then
            result.Success.Should().BeTrue();
        }

        /// <summary>
        /// Tests that a Invalid user deletion request is handled successfully.
        /// </summary>
        [Fact(DisplayName = "Given invalid user data When deleting user Then throws validation exception")]
        public async Task Handle_InvalidRequest_ThrowsException()
        {
            //Given
            var command = DeleteUserHandlerTestsData.GenerateInvalidCommand();

            //When
            var result = () => _handler.Handle(command, CancellationToken.None);

            //Then
            await result.Should().ThrowAsync<FluentValidation.ValidationException>();
            await _userRepository.DidNotReceive().DeleteAsync(Arg.Any<Guid>());
        }

        /// <summary>
        /// Tests that a Invalid user deletion request is handled successfully.
        /// </summary>
        [Fact(DisplayName = "Given invalid user data When deleting user Then throws KeyNotFoundException")]
        public async Task Handle_UserNotFound_ThrowsException()
        {
            //Given
            var command = DeleteUserHandlerTestsData.GenerateCommand();
            _userRepository.DeleteAsync(Arg.Any<Guid>()).Returns(false);
            //When
            var result = () => _handler.Handle(command, CancellationToken.None);

            //Then
            await result.Should().ThrowAsync<KeyNotFoundException>();
        }
    }
}
