using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository : IBaseRepository<Sale>
    {
        public Task<Sale?> GetSaleByCartId(Guid cartId);
        public Task<Sale?> GetSaleByIdWithAllDependenciesAsync(Guid saleId, bool tracking = false,CancellationToken cancellationToken = default);
        public IQueryable<Sale>? ListSalesWithAllDependencies(string order, bool tracking = false);
    }
}
