using Ambev.DeveloperEvaluation.Application.Users.GetUser;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers
{
    public class ListUsersResult
    {
        public IQueryable<GetUserResult>? Users { get; set; }
    }
}
