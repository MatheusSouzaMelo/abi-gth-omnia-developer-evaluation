using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CartProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

public class CreateCartRequest
{
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public List<CartProductRequest> Products { get; set; } = [];
}