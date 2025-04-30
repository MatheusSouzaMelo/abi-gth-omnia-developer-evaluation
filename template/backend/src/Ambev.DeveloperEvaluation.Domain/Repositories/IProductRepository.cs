using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        /// <summary>
        /// Retrieves all products by their category
        /// </summary>
        /// <param name="category">The product category to search for</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The products if found, null otherwise</returns>
        Task<List<Product>?> GetByCategoryAsync(string category, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all products
        /// </summary>
        /// <param name="order">query order params</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The product if found, null otherwise</returns>
        IQueryable<Product?> ListProducts(string order, string category, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all categories
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The categories if found, null otherwise</returns>
        Task<IEnumerable<string>?> ListCategoriesAsync(CancellationToken cancellationToken = default);
    }
}
