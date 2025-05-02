namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSaleProducts
{
    public class UpdateSaleProductItemRequest
    {        
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public bool Canceled { get; set; }
    }
}
