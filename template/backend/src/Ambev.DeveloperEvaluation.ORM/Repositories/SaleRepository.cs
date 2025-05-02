using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Common;
using Ambev.DeveloperEvaluation.ORM.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : BaseRepository<Sale>, ISaleRepository
    {
        private readonly DefaultContext _context;
        public SaleRepository(DefaultContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Sale?> GetSaleByCartId(Guid cartId)
        {
            return await _context.Sales.AsNoTracking().FirstOrDefaultAsync(s => s.CartId == cartId);
        }

        public async Task<Sale?> GetSaleByIdWithAllDependenciesAsync(Guid saleId, bool tracking = false, CancellationToken cancellationToken = default)
        {
            var query = _context.Sales
                 .Include(s => s.Cart)
                 .Include(s => s.Products)
                 .ThenInclude(sp => sp.CartProduct)
                 .ThenInclude(cp => cp.Product)
                 .Include(s => s.Customer)
                 .AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

             return await query.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public IQueryable<Sale>? ListSalesWithAllDependencies(string order, bool tracking = false)
        {
            var query = _context.Sales
                 .Include(s => s.Cart)
                 .ThenInclude(s => s.Products)
                 .Include(s => s.Products)
                 .Include(s => s.Customer).AsQueryable();

            if (!tracking)
                query = query.AsNoTracking();

            if (!string.IsNullOrEmpty(order))
                return OrderQueryHelper.OrderQuery(query, order);

            return query;

        }
    }
}
