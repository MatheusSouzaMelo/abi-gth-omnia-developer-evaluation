using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.ListProducts;
using Ambev.DeveloperEvaluation.WebApi.Common;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProduct
{
    public class ListProductProfile : Profile
    {
        public ListProductProfile()
        {
            CreateMap<PaginatedListRequest, PaginatedListCommand<ListProductsResult>>();
            CreateMap<GetProductResult, ListProductResponse>();
        }
    }
}
