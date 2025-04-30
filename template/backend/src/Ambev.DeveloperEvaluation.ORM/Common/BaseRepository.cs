using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Common
{
    /// <summary>
    /// Implementation of BaseRepository using Entity Framework Core
    /// </summary>
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly DbContext _context;

        /// <param name="context">The database context</param>
        public BaseRepository(DbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Creates a new entity in the database
        /// </summary>
        /// <param name="entity">The entity to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created entity</returns>
        public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _context.Set<T>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        /// <summary>
        /// Retrieves a entity by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the user</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The user if found, null otherwise</returns>
        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }


        /// <summary>
        /// Get all entities
        /// </summary>
        /// <param name="order"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>entities if any</returns>
        public IQueryable<T> GetAll(string order, CancellationToken cancellationToken = default)
        {
            var query = _context.Set<T>().AsQueryable().AsNoTracking();
            if (!string.IsNullOrEmpty(order))
                return OrderQueryHelper.OrderQuery(query, order);

            return query;
        }

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>the updated entity</returns>
        public async Task<T?> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Update(entity);
            var sucess = await _context.SaveChangesAsync(cancellationToken);

            return sucess <= 0 ? null : entity;
        }

        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>true if the entity was deleted, false if not found</returns>
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity == null)
                return false;

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }




    }
}
