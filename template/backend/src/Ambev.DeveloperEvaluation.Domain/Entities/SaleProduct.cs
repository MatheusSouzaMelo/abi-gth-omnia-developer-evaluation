using Ambev.DeveloperEvaluation.Domain.Common;
using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleProduct : BaseEntity
    {
        private decimal _discount;
        private decimal _totalAmount;

        public Guid CartProductId {  get; set; }

        [JsonIgnore]
        public CartProduct CartProduct { get; set; }
        public decimal UnitPrice { get; set; }
        public bool Canceled { get; set; } = false;
        public decimal Discount
        {
            get => _discount;
            private set => _discount = value;
        }

        public decimal TotalAmount
        {
            get => _totalAmount;
            private set => _totalAmount = value;
        }
        public Guid SaleId { get; set; }

        public void CalculateValues()
        {
            Discount = CalculateDiscount();
            TotalAmount = (CartProduct.Quantity * UnitPrice) - Discount;
        }

        private decimal CalculateDiscount()
        {
            if (CartProduct.Quantity > 10)
                return CartProduct.Quantity * UnitPrice * 0.20m;

            if (CartProduct.Quantity > 4)
                return CartProduct.Quantity * UnitPrice * 0.10m;

            return 0;
        }
    }
}
