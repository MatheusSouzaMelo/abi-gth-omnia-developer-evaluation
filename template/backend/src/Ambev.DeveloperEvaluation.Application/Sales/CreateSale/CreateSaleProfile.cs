using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<Sale, CreateSaleResult>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.IsCancelled ? "Cancelled" : "Not Cancelled"));

            CreateMap<SaleProduct, SaleProductResult>()
               .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.CartProduct.ProductId))
               .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.CartProduct.Product.Title))
               .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.CartProduct.Quantity))
               .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount));
                

            CreateMap<User, SaleCustormerResult>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.Name.Firstname} {src.Name.Lastname}"));
        }
    }
}
