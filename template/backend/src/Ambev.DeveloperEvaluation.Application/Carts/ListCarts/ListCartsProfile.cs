using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCarts
{
    public class ListCartsProfile : Profile
    {
        public ListCartsProfile()
        {
            CreateMap<Cart, GetCartResult>();
        }
    }
}
