using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using System.Numerics;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _repository;
        private readonly IMapper _mapper;

        public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _repository = saleRepository;
            _mapper = mapper;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleCommandValidator();
            var validationResult = validator.Validate(command);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingSale = await _repository.GetByIdAsync(command.Id, cancellationToken, true) ?? throw new KeyNotFoundException($"Sale with ID {command.Id} not found");

            var updatedSale = await _repository.UpdateAsync(existingSale, cancellationToken) ?? throw new KeyNotFoundException($"Error updating sale {command.Id}");

            var updatesSaleComplete = await _repository.GetSaleByIdWithAllDependenciesAsync(updatedSale.Id, cancellationToken: cancellationToken);

            Console.WriteLine($"channel.BasicPublish(exchange: 'exchange', routingKey: 'Sales', 'body: Encoding.UTF8.GetBytes('{JsonSerializer.Serialize(updatedSale)}'))");

            return _mapper.Map<UpdateSaleResult>(updatesSaleComplete);
        }
    }
}

