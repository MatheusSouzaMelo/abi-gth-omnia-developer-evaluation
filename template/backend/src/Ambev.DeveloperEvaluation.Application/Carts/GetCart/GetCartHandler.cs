using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart
{
    public class GetCartHandler : IRequestHandler<GetCartCommand, GetCartResult>
    {
        private readonly ICartRepository _repository;
        private readonly IMapper _mapper;

        public GetCartHandler(ICartRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetCartResult> Handle(GetCartCommand command, CancellationToken cancellationToken)
        {
            var validator = new GetCartValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var cart = await _repository.GetByIdAsync(command.Id, cancellationToken, false, c => c.Products);

            return cart == null ? throw new KeyNotFoundException($"Cart with ID {command.Id} not found") : _mapper.Map<GetCartResult>(cart);
        }
    }
}
