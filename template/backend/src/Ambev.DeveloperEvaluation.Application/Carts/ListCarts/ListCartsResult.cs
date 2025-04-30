using Ambev.DeveloperEvaluation.Application.Carts.GetCart;

namespace Ambev.DeveloperEvaluation.Application.Carts.ListCarts
{
    public class ListCartsResult
    {
        public IQueryable<GetCartResult>? Carts { get; set; }
    }
}
