using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users
{
    public class GetUserHandlerTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly GetUserHandler _handler;
        public GetUserHandlerTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetUserHandler(_userRepository, _mapper);
        }

        ///<summary>
        /// Tests that a valid user get request is handled successfully.
        ///</summary>
        [Fact(DisplayName = "Given valid user data When getting user Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            //Given
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = "UserName",
                Password = "Teste@12",
                Email = "Teste@teste.com",
                Phone = "119000000000",
                Status = UserStatus.Active,
                Role = UserRole.Customer
            };

            var expectedUser = new GetUserResult()
            {
                Id = user.Id,
                Email = user.Email,
                Name = "User",
                Phone = user.Phone,
                Role = user.Role,
                Status = user.Status,
            };

            var command = GetUserHandlerTestData.GenerateCommand();
            _userRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(user);
            _mapper.Map<GetUserResult>(user).Returns(expectedUser);

            //When
            var result = await _handler.Handle(command, CancellationToken.None);

            //Then
            result.Should().NotBeNull();
            result.Should().Be(expectedUser);
            await _userRepository.Received().GetByIdAsync(Arg.Any<Guid>());
        }

        /// <summary>
        /// Tests that a Invalid user get request is handled successfully.
        /// </summary>
        [Fact(DisplayName = "Given invalid user data When getting user Then throws validation exception")]
        public async Task Handle_InvalidRequest_ThrowsException()
        {
            //Given
            var command = GetUserHandlerTestData.GenerateInvalidCommand();

            //When
            var result = () => _handler.Handle(command, CancellationToken.None);

            //Then
            await result.Should().ThrowAsync<FluentValidation.ValidationException>();
            await _userRepository.DidNotReceive().DeleteAsync(Arg.Any<Guid>());
        }

        /// <summary>
        /// Tests that a Invalid user get request is handled successfully.
        /// </summary>
        [Fact(DisplayName = "Given invalid user data When getting user Then throws KeyNotFoundException")]
        public async Task Handle_UserNotFound_ThrowsException()
        {
            //Given
            var command = GetUserHandlerTestData.GenerateCommand();
            _userRepository.GetByIdAsync(Arg.Any<Guid>()).ReturnsNull();
            //When
            var result = () => _handler.Handle(command, CancellationToken.None);

            //Then
            await result.Should().ThrowAsync<KeyNotFoundException>();
        }

    }
}
