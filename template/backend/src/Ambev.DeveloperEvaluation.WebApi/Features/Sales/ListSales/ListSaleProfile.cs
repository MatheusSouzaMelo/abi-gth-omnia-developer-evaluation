using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales
{
    public class ListSaleProfile : Profile
    {
        public ListSaleProfile()
        {
            CreateMap<PaginatedListRequest, PaginatedListCommand<ListSalesResult>>();

            CreateMap<GetSaleResult, GetSaleResponse>();

            CreateMap<ListSalesResult, ListSalesResponse>();
        }
    }
}
