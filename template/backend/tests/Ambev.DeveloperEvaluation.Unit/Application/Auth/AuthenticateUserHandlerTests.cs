using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Users;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Auth
{
    /// <summary>
    /// Contains unit tests for the <see cref="AuthenticateUserHandler"/> class.
    /// </summary>
    public class AuthenticateUserHandlerTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly AuthenticateUserHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticateUserHandlerTests"/> class.
        /// Sets up the test dependencies and creates fake data generators.
        /// </summary>
        public AuthenticateUserHandlerTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _passwordHasher = Substitute.For<IPasswordHasher>();
            _jwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
            _handler = new AuthenticateUserHandler(_userRepository, _passwordHasher, _jwtTokenGenerator);
        }

        /// <summary>
        /// Tests that a valid user authentication request is handled successfully.
        /// </summary>
        [Fact(DisplayName = "Given valid user data When authenticating user Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var command = AuthenticateUserHandlerTestData.GenerateCommand();

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "UserName",
                Password = command.Password,
                Email = command.Email,
                Phone = "11900000000",
                Status = UserStatus.Active,
                Role = UserRole.Admin,
            };

            _userRepository.GetByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(user);

            _passwordHasher.VerifyPassword(Arg.Any<string>(), Arg.Any<string>()).Returns(true);

            _jwtTokenGenerator.GenerateToken(Arg.Any<User>()).Returns("token");

            // When
            var authenticateUserResult = await _handler.Handle(command, CancellationToken.None);

            // Then
            authenticateUserResult.Should().NotBeNull();
            authenticateUserResult.Token.Should().NotBeEmpty();
            authenticateUserResult.Email.Should().Be(command.Email);
            authenticateUserResult.Role.Should().Be(nameof(UserRole.Admin));
        }

        /// <summary>
        /// Tests that a invalid user email authentication request is handled successfully.
        /// </summary>
        [Fact(DisplayName = "Given invalid user email When authenticating user Then throws exception")]
        public async Task Handle_InvalidEmailRequest_ThrowsException()
        {
            // Given
            var command = AuthenticateUserHandlerTestData.GenerateCommand();

            _userRepository.GetByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .ReturnsNull();

            _passwordHasher.VerifyPassword(Arg.Any<string>(), Arg.Any<string>()).Returns(true);

            // When
            var result = () => _handler.Handle(command, CancellationToken.None);

            //Then
            await result.Should().ThrowAsync<UnauthorizedAccessException>();
        }

        /// <summary>
        /// Tests that a invalid user password authentication request is handled successfully.
        /// </summary>
        [Fact(DisplayName = "Given invalid user password When authenticating user Then throws exception")]
        public async Task Handle_InvalidPasswordRequest_ThrowsException()
        {
            // Given
            var command = AuthenticateUserHandlerTestData.GenerateCommand();

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "UserName",
                Password = command.Password,
                Email = command.Email,
                Phone = "11900000000",
                Status = UserStatus.Active,
                Role = UserRole.Admin,
            };

            _userRepository.GetByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(user);

            _passwordHasher.VerifyPassword(Arg.Any<string>(), Arg.Any<string>()).Returns(false);

            // When
            var result = () => _handler.Handle(command, CancellationToken.None);

            //Then
            await result.Should().ThrowAsync<UnauthorizedAccessException>();

        }


        /// <summary>
        /// Tests that a not active user authentication request is handled successfully.
        /// </summary>
        [Theory(DisplayName = "Given invalid user password When authenticating user Then throws exception")]
        [InlineData(UserStatus.Suspended)]
        [InlineData(UserStatus.Inactive)]
        [InlineData(UserStatus.Unknown)]
        public async Task Handle_InvalidStatusRequest_ThrowsException(UserStatus userStatus)
        {
            // Given
            var command = AuthenticateUserHandlerTestData.GenerateCommand();

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "UserName",
                Password = command.Password,
                Email = command.Email,
                Phone = "11900000000",
                Status = userStatus,
                Role = UserRole.Admin,
            };

            _userRepository.GetByEmailAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(user);

            _passwordHasher.VerifyPassword(Arg.Any<string>(), Arg.Any<string>()).Returns(true);

            // When
            var result = () => _handler.Handle(command, CancellationToken.None);

            //Then
            await result.Should().ThrowAsync<UnauthorizedAccessException>();
        }
    }
}
