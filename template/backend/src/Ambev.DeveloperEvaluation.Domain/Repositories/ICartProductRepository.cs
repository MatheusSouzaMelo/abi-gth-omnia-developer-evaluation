using Ambev.DeveloperEvaluation.Domain.Entities;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ICartProductRepository : IBaseRepository<CartProduct>
    {
        Task<IEnumerable<CartProduct>> ListCartProductsByIds(IEnumerable<Guid> cartProductIds, bool tracking = false, 
            CancellationToken cancellationToken = default, params Expression<Func<CartProduct, object>>[] includes);
    }
}
