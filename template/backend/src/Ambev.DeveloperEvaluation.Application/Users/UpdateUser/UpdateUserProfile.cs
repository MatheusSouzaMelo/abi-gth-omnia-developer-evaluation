using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser
{
    /// <summary>
    /// Profile for mapping between UpdateUserCommand and user entity
    /// </summary>
    public class UpdateUserProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for UpdateUserCommand operation
        /// </summary>
        public UpdateUserProfile()
        {
            CreateMap<UpdateUserCommand, User>();
            CreateMap<User, UpdateUserResult>();
        }
    }
}
