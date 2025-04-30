using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;
using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
using Ambev.DeveloperEvaluation.Application.Carts.ListCarts;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Provides methods for generating test data for ListUsersHandler tests using the Bogus library.
/// This class centralizes all test data generation to ensure consistency across test cases.
/// </summary>
public static class ListUsersHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid ListUsersCommand entities.
    /// The generated commands will have valid order parameters.
    /// </summary>
    private static readonly Faker<PaginatedListCommand<ListUsersResult>> listUsersCommandFaker = new Faker<PaginatedListCommand<ListUsersResult>>()
        .RuleFor(c => c.Order, f => f.PickRandom("name", "email", "createdAt", "status"));

    /// <summary>
    /// Configures the Faker to generate valid User entities for repository responses.
    /// </summary>
    private static readonly Faker<User> userFaker = new Faker<User>()
        .RuleFor(u => u.Id, f => Guid.NewGuid())
        .RuleFor(u => u.Username, f => f.Internet.UserName())
        .RuleFor(u => u.Email, f => f.Internet.Email())
        .RuleFor(u => u.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}")
        .RuleFor(u => u.Status, f => f.PickRandom<UserStatus>())
        .RuleFor(u => u.Role, f => f.PickRandom<UserRole>())
        .RuleFor(u => u.CreatedAt, f => f.Date.Past())
        .RuleFor(u => u.Name, f => new Name
        {
            Firstname = f.Name.FirstName(),
            Lastname = f.Name.LastName()
        })
        .RuleFor(u => u.Address, f => new Address
        {
            City = f.Address.City(),
            Street = f.Address.StreetName(),
            Number = f.Random.Number(1, 9999),
            Zipcode = f.Address.ZipCode(),
            Geolocation = new Geolocation
            {
                Lat = f.Address.Latitude().ToString("F6"),
                Long = f.Address.Longitude().ToString("F6")
            }
        });

    /// <summary>
    /// Configures the Faker to generate valid GetUserResult entities for mapper responses.
    /// </summary>
    private static readonly Faker<GetUserResult> getUserResultFaker = new Faker<GetUserResult>()
        .RuleFor(u => u.Id, f => Guid.NewGuid())
        .RuleFor(u => u.Email, f => f.Internet.Email())
        .RuleFor(u => u.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}")
        .RuleFor(u => u.Status, f => f.PickRandom<UserStatus>())
        .RuleFor(u => u.Role, f => f.PickRandom<UserRole>())
        .RuleFor(u => u.Name, f => new Name
        {
            Firstname = f.Name.FirstName(),
            Lastname = f.Name.LastName()
        });

    /// <summary>
    /// Generates a valid ListUsersCommand with randomized order parameter.
    /// </summary>
    public static PaginatedListCommand<ListUsersResult> GenerateValidCommand()
    {
        return listUsersCommandFaker.Generate();
    }

    /// <summary>
    /// Generates a ListUsersCommand with empty order parameter.
    /// </summary>
    public static PaginatedListCommand<ListUsersResult> GenerateEmptyOrderCommand()
    {
        return new PaginatedListCommand<ListUsersResult> { Order = string.Empty };
    }

    /// <summary>
    /// Generates a list of User entities for repository responses.
    /// </summary>
    /// <param name="count">Number of users to generate</param>
    public static List<User> GenerateUsers(int count)
    {
        return userFaker.Generate(count);
    }

    /// <summary>
    /// Generates a list of GetUserResult entities for mapper responses.
    /// </summary>
    /// <param name="count">Number of results to generate</param>
    public static List<GetUserResult> GenerateUserResults(int count)
    {
        return getUserResultFaker.Generate(count);
    }

    /// <summary>
    /// Generates an empty list of User entities.
    /// </summary>
    public static List<User> GenerateEmptyUsersList()
    {
        return new List<User>();
    }
}