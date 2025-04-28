using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IUserRepository using Entity Framework Core
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of UserRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public UserRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new user in the database
    /// </summary>
    /// <param name="user">The user to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user</returns>
    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    /// <summary>
    /// Retrieves a user by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves a user by their email address
    /// </summary>
    /// <param name="email">The email address to search for</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users.AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    /// <summary>
    /// Deletes a user from the database
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the user was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await GetByIdAsync(id, cancellationToken);
        if (user == null)
            return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public IQueryable<User> ListUsers(string order, CancellationToken cancellationToken = default)
    {
        var query = _context.Users.AsQueryable().AsNoTracking();
        if (string.IsNullOrEmpty(order))
            return query.OrderBy(u => u.CreatedAt);

        return OrderQuery(query, order);
    }


    private static IQueryable<User> OrderQuery(IQueryable<User> query, string order)
    {
        var parameters = order.Split(',');

        foreach (var parameter in parameters)
        {
            var command = parameter.Trim().Split(' ');
            var property = command[0].Trim().ToLower();
            var direction = command[1].Trim().ToLower();

            if (direction != "asc" && direction != "desc")
                continue;

            switch (property)
            {
                case "username":
                    query = direction == "desc"
                        ? query.OrderByDescending(u => u.Username)
                        : query.OrderBy(u => u.Username);
                    break;

                case "email":
                    query = direction == "desc"
                        ? query.OrderByDescending(u => u.Email)
                        : query.OrderBy(u => u.Email);
                    break;

                case "createdat":
                    query = direction == "desc"
                        ? query.OrderByDescending(u => u.CreatedAt)
                        : query.OrderBy(u => u.CreatedAt);
                    break;

                case "status":
                    query = direction == "desc"
                        ? query.OrderByDescending(u => u.Status)
                        : query.OrderBy(u => u.Status);
                    break;

                case "role":
                    query = direction == "desc"
                        ? query.OrderByDescending(u => u.Role)
                        : query.OrderBy(u => u.Role);
                    break;

                case "firstname":
                    query = direction == "desc"
                        ? query.OrderByDescending(u => u.Name.Firstname)
                        : query.OrderBy(u => u.Name.Firstname);
                    break;

                default:
                    break;
            }
        }
        return query;
    }

    public async Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        var updatedUser = _context.Update(user);
        var sucess = await _context.SaveChangesAsync(cancellationToken);

        if (sucess <= 0)
            return null;

        return updatedUser.Entity;
    }
}
