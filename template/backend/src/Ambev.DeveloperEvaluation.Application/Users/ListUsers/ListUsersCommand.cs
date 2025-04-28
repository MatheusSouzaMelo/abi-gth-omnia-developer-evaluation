using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers
{
    public class ListUsersCommand : IRequest<ListUsersResult>
    {
        public string Order { get; set; } = string.Empty;
    }
}
