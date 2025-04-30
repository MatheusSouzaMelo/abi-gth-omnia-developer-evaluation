using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, DeleteProductResult>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public DeleteProductHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var deleteProductValidator = new DeleteProductValidator();
            var validationResult = deleteProductValidator.Validate(command);

            if(!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var product = await _productRepository.GetByIdAsync(command.Id, cancellationToken) ?? throw new KeyNotFoundException($"Prodcut with ID {command.Id} not found");

            var sucess = await _productRepository.DeleteAsync(command.Id, cancellationToken);

            return new DeleteProductResult() { Success = sucess };
        }
    }
}
