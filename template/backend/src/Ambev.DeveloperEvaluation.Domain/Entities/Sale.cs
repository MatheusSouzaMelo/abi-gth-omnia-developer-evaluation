using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public string SaleNumber { get; set; } = string.Empty;
        public DateTime SaleDate { get; set; }
        public Guid? CustomerId { get; set; }
        public User Customer { get; set; }
        public string Branch { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public bool IsCancelled { get; set; }
        public Guid CartId { get; set; }
        public Cart Cart { get; set; }
        public List<SaleProduct> Products { get; set; } = [];
    }
}
