namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequest
    {
        public Guid CartId { get; set; }
        public string Branch { get; set; } = string.Empty;
    }
}
