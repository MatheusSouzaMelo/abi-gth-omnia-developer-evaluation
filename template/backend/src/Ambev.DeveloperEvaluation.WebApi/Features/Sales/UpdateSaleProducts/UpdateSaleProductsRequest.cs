using Ambev.DeveloperEvaluation.Application.Sales.UpdateSaleProducts;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSaleProducts
{
    public class UpdateSaleProductsRequest
    {
        public List<UpdateSaleProductItemRequest> Itens { get; set; } = [];
    }
}
