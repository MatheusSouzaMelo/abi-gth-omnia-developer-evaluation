using Ambev.DeveloperEvaluation.Application.Carts;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Common
{
    public static class ValidateCartProductsHelper
    {
        public static async Task<(bool areAllProductsValid, string message)> ValidateCartProductsAsync(IProductRepository repository, List<CartProductCommand> command, CancellationToken cancellationToken)
        {
            var existingProducts = await repository.ListProductsByIdsAsync(command.Select(p => p.ProductId), false, cancellationToken) ?? throw new KeyNotFoundException($"No products found");
            var notFoundProductIds = new List<Guid>();
            var message = string.Empty;
            if (existingProducts.Count() != command.Count)
            {
                var productsIds = command.Select(p => p.ProductId).ToList();
                foreach (var productId in productsIds)
                {
                    if (existingProducts.Any(p => p.Id != productId))
                        notFoundProductIds.Add(productId);
                }

                message = $"Products with id  {string.Join(", ", notFoundProductIds)} not found";
            }

            return (notFoundProductIds.Count <= 0, message);
        }
    }
}
