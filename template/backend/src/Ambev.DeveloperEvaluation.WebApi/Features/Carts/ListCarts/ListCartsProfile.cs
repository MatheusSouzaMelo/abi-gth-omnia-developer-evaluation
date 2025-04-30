using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Carts.ListCarts;
using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.ListCarts
{
    public class ListCartsProfile : Profile
    {
        public ListCartsProfile()
        {
            CreateMap<PaginatedListRequest, PaginatedListCommand<ListCartsResult>>();
            CreateMap<GetCartResult, PaginatedResponse<GetCartResponse>>();
        }
    }
}
