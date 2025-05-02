using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    internal class GetSaleHandler : IRequestHandler<GetSaleCommand, GetSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public GetSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public  async Task<GetSaleResult> Handle(GetSaleCommand command, CancellationToken cancellationToken)
        {
            var getSaleCommandValidator = new GetSaleCommandValidator();
            var validationResult = getSaleCommandValidator.Validate(command);

            if(!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetSaleByIdWithAllDependenciesAsync(command.SaleId, cancellationToken: cancellationToken) ?? throw new KeyNotFoundException($"Sale with id {command.SaleId} not found");
           
            return _mapper.Map<GetSaleResult>(sale);
        }
    }
}
