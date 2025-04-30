using Ambev.DeveloperEvaluation.Application.Products.GetProduct;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts
{
    public class ListProductsResult
    {
        public IQueryable<GetProductResult>? Products { get; set; }
    }
}
