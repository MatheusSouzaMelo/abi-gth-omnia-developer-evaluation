using Ambev.DeveloperEvaluation.Application.Sales;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSaleProducts;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSaleProducts
{
    public class UpdateSaleProductsProfile : Profile
    {
        public UpdateSaleProductsProfile()
        {

            CreateMap<UpdateSaleProductsRequest, UpdateSaleProductsCommand>();

            CreateMap<UpdateSaleProductsResult, UpdateSaleProductsResponse>();
            CreateMap<SaleCustormerResult, SaleCustomerResponse>();
            CreateMap<SaleProductResult, SaleProductResponse>();

        }
    }
}
