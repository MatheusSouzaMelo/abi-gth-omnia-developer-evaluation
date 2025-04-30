using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Common;
using Ambev.DeveloperEvaluation.ORM.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly DefaultContext _context;

        public ProductRepository(DefaultContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<List<Product>?> GetByCategoryAsync(string category, CancellationToken cancellationToken = default)
        {
            return await _context.Products.AsNoTracking().Where(o => o.Category == category).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<string>?> ListCategoriesAsync(CancellationToken cancellationToken = default)
        {
            
            return await _context.Products.AsNoTracking().Select(p => p.Category).Distinct().ToListAsync(cancellationToken);
        }

        public IQueryable<Product?> ListProducts(string order, string category, CancellationToken cancellationToken = default)
        {
            var query = GetAll(order, cancellationToken);

            if (!string.IsNullOrEmpty(category))
                query = query.Where(p => p.Category.ToLower().Trim() == category);

            return OrderQueryHelper.OrderQuery(query, order);
        }

        public async Task<IEnumerable<Product>?> ListProductsByIdsAsync(IEnumerable<Guid> ProductIds, bool tracking = false, CancellationToken cancellationToken = default)
        {
            var query = _context.Products.Where(p => ProductIds.Contains(p.Id));

            if(!tracking)
                query.AsNoTracking();

            return await query.ToListAsync(cancellationToken);
        }
    }
}
