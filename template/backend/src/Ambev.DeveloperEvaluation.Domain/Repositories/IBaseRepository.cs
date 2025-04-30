using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Interface for generic repository operations
    /// </summary>
    /// <typeparam name="T">Type of entity, must inherit from BaseEntity</typeparam>

    public interface IBaseRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Creates a new entity in the database
        /// </summary>
        /// <param name="entity">The entity to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created entity</returns>
        Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves an entity by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the entity</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The entity if found, null otherwise</returns>
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all entities with optional ordering
        /// </summary>
        /// <param name="order">Ordering string (e.g., "Name asc, Date desc")</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Queryable collection of entities</returns>
        IQueryable<T> GetAll(string order, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing entity
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The updated entity if successful, null otherwise</returns>
        Task<T?> UpdateAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an entity by its ID
        /// </summary>
        /// <param name="id">The ID of the entity to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if the entity was deleted, false if not found</returns>
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    }
}
