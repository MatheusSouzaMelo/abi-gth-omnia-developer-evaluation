using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users
{
    public class UpdateUserHandlerTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly UpdateUserHandler _handler;

        public UpdateUserHandlerTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _mapper = Substitute.For<IMapper>();
            _passwordHasher = Substitute.For<IPasswordHasher>();
            _handler = new UpdateUserHandler(_userRepository, _mapper, _passwordHasher);
        }

        ///<summary>
        /// Tests that a valid user update request is handled successfully with all properties mapped and validated.
        ///</summary>
        [Fact(DisplayName = "Given valid user data When updating user Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var command = UpdateUserHandlerTestData.GenerateValidCommand();
            var existingUser = new User { Id = command.Id, Email = "old@email.com" };
            var updatedUser = new User { Id = command.Id };
            var expectedResult = new UpdateUserResult { Id = command.Id };

            _userRepository.GetByEmailAsync(command.Email).Returns(existingUser);
            _passwordHasher.HashPassword(command.Password).Returns("hashedPassword");
            _mapper.Map<User>(command).Returns(updatedUser);
            _userRepository.UpdateUserAsync(updatedUser).Returns(updatedUser);
            _mapper.Map<UpdateUserResult>(updatedUser).Returns(expectedResult);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Should().Be(expectedResult);
            await _userRepository.Received().GetByEmailAsync(command.Email);
            await _userRepository.Received().UpdateUserAsync(updatedUser);
            _passwordHasher.Received().HashPassword(command.Password);
        }

        ///<summary>
        /// Tests that updating to an existing email throws InvalidOperationException.
        ///</summary>
        [Fact(DisplayName = "Given existing email When updating user Then throws InvalidOperationException")]
        public async Task Handle_ExistingEmail_ThrowsException()
        {
            // Given
            var command = UpdateUserHandlerTestData.GenerateValidCommand();
            var existingUserWithEmail = new User { Id = Guid.NewGuid(), Email = command.Email };

            _userRepository.GetByEmailAsync(command.Email).Returns(existingUserWithEmail);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage($"User with email {command.Email} already exists");
            await _userRepository.DidNotReceiveWithAnyArgs().UpdateUserAsync(Arg.Any<User>(), default);
        }

        ///<summary>
        /// Tests that updating non-existent user throws KeyNotFoundException.
        ///</summary>
        [Fact(DisplayName = "Given non-existent user When updating user Then throws KeyNotFoundException")]
        public async Task Handle_UserNotFound_ThrowsException()
        {
            // Given
            var command = UpdateUserHandlerTestData.GenerateValidCommand();

            _userRepository.GetByEmailAsync(command.Email).ReturnsNull();

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"User with ID {command.Id} not found");
            await _userRepository.DidNotReceive().UpdateUserAsync(Arg.Any<User>());
        }
    }
}