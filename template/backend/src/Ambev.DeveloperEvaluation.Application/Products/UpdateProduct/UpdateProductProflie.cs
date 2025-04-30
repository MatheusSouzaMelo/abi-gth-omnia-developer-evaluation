using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductProflie : Profile
    {
        public UpdateProductProflie()
        {
            CreateMap<UpdateProductCommand, Product>();
            CreateMap<Product, UpdateProductResult>();
        }
    }
}
