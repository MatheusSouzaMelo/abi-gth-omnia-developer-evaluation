namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSaleProducts
{
    public class UpdateSaleProductsItemRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public bool Canceled { get; set; }
    }
}
