using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public static class UpdateUserHandlerTestData
    {
        private static readonly Faker<UpdateUserCommand> ValidCommandFaker = new Faker<UpdateUserCommand>()
            .RuleFor(u => u.Id, f => Guid.NewGuid())
            .RuleFor(u => u.Username, f => f.Internet.UserName())
            .RuleFor(u => u.Password, f => $"Valid@123{f.Random.Number(100, 999)}")
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}")
            .RuleFor(u => u.Status, f => f.PickRandom<UserStatus>(UserStatus.Active, UserStatus.Inactive, UserStatus.Suspended))
            .RuleFor(u => u.Role, f => f.PickRandom<UserRole>(UserRole.Admin, UserRole.Customer))
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
                Zipcode = f.Address.ZipCode()
            });

        public static UpdateUserCommand GenerateValidCommand()
        {
            return ValidCommandFaker.Generate();
        }
    }
}
