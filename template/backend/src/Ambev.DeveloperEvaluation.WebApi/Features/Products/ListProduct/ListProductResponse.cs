using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProduct
{
    public class ListProductResponse
    {
        public IQueryable<GetProductResponse>? Products { get; set; }

    }
}
