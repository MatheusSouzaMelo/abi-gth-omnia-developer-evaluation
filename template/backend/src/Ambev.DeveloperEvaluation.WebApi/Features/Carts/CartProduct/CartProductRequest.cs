namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CartProduct
{
    public class CartProductRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
