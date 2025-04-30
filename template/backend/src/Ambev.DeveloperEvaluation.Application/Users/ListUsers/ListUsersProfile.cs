using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers
{
    /// <summary>
    /// Profile for mapping between User entity and GetUserResponse
    /// </summary>
    public class ListUsersProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for ListUser operation
        /// </summary>
        public ListUsersProfile()
        {
            CreateMap<User, GetUserResult>();
        }
    }
}
