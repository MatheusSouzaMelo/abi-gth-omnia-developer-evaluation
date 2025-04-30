using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct
{
    public class UpdateProductProfile : Profile
    {
        public UpdateProductProfile()
        {
            CreateMap<UpdateProductRequest, UpdateProductCommand>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom((src, dest, _, context) => context.Items["Id"]));

            CreateMap<UpdateProductResult, UpdateProductResponse>();
        }
    }
}
