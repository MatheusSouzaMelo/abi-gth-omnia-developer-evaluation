using Ambev.DeveloperEvaluation.Application.Products.Category;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListCategories
{
    public class ListCategoriesProfile : Profile
    {
        public ListCategoriesProfile()
        {
            CreateMap<ListCategoriesResult, ListCategoriesResponse>();
        }
    }
}
