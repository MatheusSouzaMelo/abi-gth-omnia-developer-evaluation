using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class CartProductRepository : BaseRepository<CartProduct>, ICartProductRepository
    {
        private readonly DefaultContext _context;
        public CartProductRepository(DefaultContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartProduct>> ListCartProductsByIds(IEnumerable<Guid> cartProductIds,
            bool tracking = false, CancellationToken cancellationToken = default,
            params Expression<Func<CartProduct, object>>[] includes)
        {
            var query = _context.CartProducts.Where(p => cartProductIds.Contains(p.Id));

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (!tracking)
                query.AsNoTracking();

            return await query.ToListAsync(cancellationToken);
        }
    }
}
