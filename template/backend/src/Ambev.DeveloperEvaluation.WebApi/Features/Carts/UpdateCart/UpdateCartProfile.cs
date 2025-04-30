using Ambev.DeveloperEvaluation.Application.Carts;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CartProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart
{
    public class UpdateCartProfile : Profile
    {
        public UpdateCartProfile()
        {
            CreateMap<UpdateCartRequest, UpdateCartCommand>();
            CreateMap<CartProductRequest, CartProductCommand>();
            CreateMap<UpdateCartResult, UpdateCartResponse>();
            CreateMap<CartProductResult, CartProductResponse>();
        }
    }
}
