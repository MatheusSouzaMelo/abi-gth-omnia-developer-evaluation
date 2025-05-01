using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts
{
    public class CartProductProfile : Profile
    {
        public CartProductProfile()
        {
            CreateMap<CartProductCommand, CartProduct>();
        }
    }
}
