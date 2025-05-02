namespace Ambev.DeveloperEvaluation.Application.Sales
{
    public class SaleProductResult
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public bool Canceled { get; set; }

    }
}
