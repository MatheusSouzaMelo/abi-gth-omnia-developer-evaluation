using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    internal class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public UpdateProductHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            UpdateProductValidator validator = new UpdateProductValidator();
            var validationResult = validator.Validate(command);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
            var productToUpdate = _mapper.Map<Product>(command);

            var product = await _repository.GetByIdAsync(command.Id, cancellationToken) ?? throw new KeyNotFoundException($"Prodcut with ID {command.Id} not found");

            var updatedProduct = await _repository.UpdateAsync(productToUpdate, cancellationToken) ?? throw new KeyNotFoundException($"Prodcut with ID {command.Id} not found");

            return _mapper.Map<UpdateProductResult>(updatedProduct);
        }
    }
}
