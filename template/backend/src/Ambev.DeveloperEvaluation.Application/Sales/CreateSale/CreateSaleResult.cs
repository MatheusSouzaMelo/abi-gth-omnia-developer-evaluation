namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleResult
    {
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }
        public SaleCustormerResult Customer { get; set; } = new();
        public decimal TotalAmount { get; set; }
        public string Branch { get; set; } = string.Empty;
        public List<SaleProductResult> Products { get; set; } = [];
        public string Status { get; set; } = string.Empty;
    }
}
