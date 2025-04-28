using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers
{
    /// <summary>
    /// Handler for processing ListUserCommand requests
    /// </summary>
    public class ListUsersHandler : IRequestHandler<ListUsersCommand, ListUsersResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of GetUserHandler
        /// </summary>
        /// <param name="userRepository">The user repository</param>
        public ListUsersHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the GetUserCommand request
        /// </summary>
        /// <param name="request">The ListUser command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The user details if found</returns>
        public Task<ListUsersResult> Handle(ListUsersCommand request, CancellationToken cancellationToken)
        {
            var userQuery = _userRepository.ListUsers(request.Order, cancellationToken);

            if (!userQuery.Any())
            {
                throw new KeyNotFoundException("No users found");
            }

            var result = _mapper.ProjectTo<GetUserResult>(userQuery);

            return Task.FromResult(new ListUsersResult
            {
                Users = result
            });
        }
    }
}
