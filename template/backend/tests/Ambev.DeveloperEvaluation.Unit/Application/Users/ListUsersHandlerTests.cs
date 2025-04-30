using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.ListUsers;

public class ListUsersHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ListUsersHandler _handler;

    public ListUsersHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new ListUsersHandler(_userRepository, _mapper);
    }

    ///<summary>
    /// Tests that users are returned with the specified ordering.
    ///</summary>
    [Fact(DisplayName = "Given valid order When listing users Then returns ordered users")]
    public async Task Handle_ValidOrder_ReturnsOrderedUsers()
    {
        // Given
        var command = ListUsersHandlerTestData.GenerateValidCommand();
        var users = ListUsersHandlerTestData.GenerateUsers(3).AsQueryable();
        var expectedResults = ListUsersHandlerTestData.GenerateUserResults(3).AsQueryable();

        _userRepository.GetAll(command.Order).Returns(users);
        _mapper.ProjectTo<GetUserResult>(users).Returns(expectedResults);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Users.Should().NotBeNull();
        result.Users.Should().HaveCount(3);
        _userRepository.Received(1).GetAll(command.Order);
    }

    ///<summary>
    /// Tests that empty result throws KeyNotFoundException with proper message.
    ///</summary>
    [Fact(DisplayName = "Given no users When listing Then throws KeyNotFoundException")]
    public async Task Handle_NoUsers_ThrowsKeyNotFoundException()
    {
        // Given
        var command = ListUsersHandlerTestData.GenerateValidCommand();
        var emptyUsers = ListUsersHandlerTestData.GenerateEmptyUsersList().AsQueryable();

        _userRepository.GetAll(command.Order).Returns(emptyUsers);

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage("No users found");
        _userRepository.Received(1).GetAll(command.Order);
    }

    ///<summary>
    /// Tests that default order is used when no order is specified.
    ///</summary>
    [Fact(DisplayName = "Given empty order When listing Then uses default order")]
    public async Task Handle_EmptyOrder_UsesDefaultOrder()
    {
        // Given
        var command = ListUsersHandlerTestData.GenerateEmptyOrderCommand();
        var users = ListUsersHandlerTestData.GenerateUsers(2).AsQueryable();
        var expectedResults = ListUsersHandlerTestData.GenerateUserResults(2).AsQueryable();

        _userRepository.GetAll(string.Empty).Returns(users);
        _mapper.ProjectTo<GetUserResult>(users).Returns(expectedResults);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Should().NotBeNull();
        result.Users.Should().NotBeNull();
        result.Users.Should().HaveCount(2);
        _userRepository.Received(1).GetAll(string.Empty);
    }

    ///<summary>
    /// Tests that all user properties are correctly mapped to the result.
    ///</summary>
    [Fact(DisplayName = "Given users When listing Then maps all properties correctly")]
    public async Task Handle_UsersExist_MapsAllPropertiesCorrectly()
    {
        // Given
        var command = ListUsersHandlerTestData.GenerateValidCommand();
        var users = ListUsersHandlerTestData.GenerateUsers(1).AsQueryable();
        var expectedResults = ListUsersHandlerTestData.GenerateUserResults(1).AsQueryable();

        _userRepository.GetAll(command.Order).Returns(users);
        _mapper.ProjectTo<GetUserResult>(users).Returns(expectedResults);

        // When
        var result = await _handler.Handle(command, CancellationToken.None);

        // Then
        result.Users.Should().NotBeNull();
        var firstUser = result.Users.First();
        var expectedUser = expectedResults.First();

        firstUser.Id.Should().Be(expectedUser.Id);
        firstUser.Email.Should().Be(expectedUser.Email);
        firstUser.Phone.Should().Be(expectedUser.Phone);
        firstUser.Status.Should().Be(expectedUser.Status);
        firstUser.Role.Should().Be(expectedUser.Role);
        firstUser.Name.Should().BeEquivalentTo(expectedUser.Name);
    }
}