using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.Category
{
    internal class ListCategoriesProfile : Profile
    {
        public ListCategoriesProfile()
        {
            CreateMap<List<string>, ListCategoriesResult>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src));
        }
    }
}
