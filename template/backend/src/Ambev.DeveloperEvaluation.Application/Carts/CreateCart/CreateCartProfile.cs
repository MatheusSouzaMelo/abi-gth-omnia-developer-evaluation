using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

public class CreateCartProfile : Profile
{
    public CreateCartProfile()
    {
        CreateMap<CreateCartCommand, Cart>();
        CreateMap<CartProductCommand, CartProduct>();
        CreateMap<Cart, CreateCartResult>();
        CreateMap<CartProduct, CartProductResult>();


    }
}
