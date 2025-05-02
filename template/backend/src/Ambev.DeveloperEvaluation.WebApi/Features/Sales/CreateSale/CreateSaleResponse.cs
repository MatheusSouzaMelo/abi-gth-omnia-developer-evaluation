namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleResponse
    {
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }
        public SaleCustomerResponse Customer { get; set; } = new();
        public decimal TotalAmount { get; set; }
        public string Branch { get; set; } = string.Empty;
        public List<SaleProductResponse> Products { get; set; } = new();
        public string IsCancelled { get; set; } = string.Empty;


    }
}
