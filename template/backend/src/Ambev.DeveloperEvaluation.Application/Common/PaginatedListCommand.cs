using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Common
{
    public class PaginatedListCommand<T> : IRequest<T>
    {
        public string By { get; set; } = string.Empty;
        public string Order { get; set; } = string.Empty;
    }
}
