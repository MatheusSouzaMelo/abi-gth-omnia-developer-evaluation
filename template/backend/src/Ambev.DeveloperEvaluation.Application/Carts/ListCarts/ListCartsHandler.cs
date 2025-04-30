using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCarts
{
    public class ListCartsHandler : IRequestHandler<PaginatedListCommand<ListCartsResult>, ListCartsResult>
    {
        private readonly ICartRepository _repository;
        private readonly IMapper _mapper;

        public ListCartsHandler(ICartRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<ListCartsResult> Handle(PaginatedListCommand<ListCartsResult> command, CancellationToken cancellationToken)
        {
            var query = _repository.GetAll(command.Order, cancellationToken, false, c => c.Products);

            if (!query.Any())
            {
                throw new KeyNotFoundException("No Cart found");
            }

            var result = _mapper.ProjectTo<GetCartResult>(query);

            return Task.FromResult(new ListCartsResult
            {
                Carts = result
            });
        }
    }
}
