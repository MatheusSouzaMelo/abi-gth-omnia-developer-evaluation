using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CartProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart
{
    public class UpdateCartRequest
    {
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public List<CartProductRequest> Products { get; set; } = [];
    }
}
