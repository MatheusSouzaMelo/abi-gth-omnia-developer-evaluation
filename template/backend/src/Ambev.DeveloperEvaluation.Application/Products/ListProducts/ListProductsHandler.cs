using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts
{
    public class ListProductsHandler : IRequestHandler<PaginatedListCommand<ListProductsResult>, ListProductsResult>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ListProductsHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public Task<ListProductsResult> Handle(PaginatedListCommand<ListProductsResult> command, CancellationToken cancellationToken)
        {
            var productsQuery = _productRepository.ListProducts(command.Order, command.By.ToLower().Trim(), cancellationToken);
            if (productsQuery == null || !productsQuery.Any())
                throw new KeyNotFoundException("No product were found");

            return Task.FromResult(new ListProductsResult { Products = _mapper.ProjectTo<GetProductResult>(productsQuery) });
        }
    }
}
