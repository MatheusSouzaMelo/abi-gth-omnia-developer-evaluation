using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser
{
    public class UpdateUserProfile : Profile
    {
        public UpdateUserProfile()
        {
            CreateMap<UpdateUserRequest, UpdateUserCommand>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom((src, dest, _, context) => context.Items["Id"]));

            CreateMap<UpdateUserResult, UpdateUserResponse>();
        }
    }
}
