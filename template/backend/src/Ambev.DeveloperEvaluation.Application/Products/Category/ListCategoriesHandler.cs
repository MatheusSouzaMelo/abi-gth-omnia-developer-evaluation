using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Category
{
    public class ListCategoriesHandler : IRequestHandler<ListCategoriesCommand, ListCategoriesResult>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ListCategoriesHandler(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<ListCategoriesResult> Handle(ListCategoriesCommand request, CancellationToken cancellationToken)
        {
            var categories = await _productRepository.ListCategoriesAsync(cancellationToken) ?? throw new KeyNotFoundException("No category found");

            return _mapper.Map<ListCategoriesResult>(categories);
        }
    }
}
