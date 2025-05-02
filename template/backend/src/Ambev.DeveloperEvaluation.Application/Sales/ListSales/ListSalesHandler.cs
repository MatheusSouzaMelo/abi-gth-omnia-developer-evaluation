using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    public class ListSalesHandler : IRequestHandler<PaginatedListCommand<ListSalesResult>, ListSalesResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public ListSalesHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public Task<ListSalesResult> Handle(PaginatedListCommand<ListSalesResult> request, CancellationToken cancellationToken)
        {
            var salesQuery = _saleRepository.ListSalesWithAllDependencies(request.Order);

            if (salesQuery == null || !salesQuery.Any())
                throw new KeyNotFoundException("No sale found");

            return Task.FromResult(new ListSalesResult() { Sales = _mapper.ProjectTo<GetSaleResult>(salesQuery) });
        }
    }
}
