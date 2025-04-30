using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.ListCarts
{
    public class ListCartsResponse
    {
        public IQueryable<GetCartResponse>? Carts { get; set; }
    }
}
