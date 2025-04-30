using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
using Ambev.DeveloperEvaluation.WebApi.Common;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers
{
    public class ListUsersProfile : Profile
    {
        public ListUsersProfile()
        {
            CreateMap<PaginatedListRequest, PaginatedListCommand<ListUsersResult>>();
            CreateMap<GetUserResult, ListUserResponse>();
        }
    }
}
