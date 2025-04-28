using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers
{
    public class ListUserResponse
    {
        public IQueryable<GetUserResponse>? Users {  get; set; }
    }
}
