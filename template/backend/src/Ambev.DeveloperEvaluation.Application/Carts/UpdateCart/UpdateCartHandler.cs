using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, UpdateCartResult>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateCartHandler(ICartRepository CartRepository, IMapper mapper, IProductRepository productRepository)
        {
            _cartRepository = CartRepository;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<UpdateCartResult> Handle(UpdateCartCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateCartCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingCart = await _cartRepository.GetByIdAsync(command.Id, cancellationToken, true, c => c.Products) ?? throw new KeyNotFoundException($"Cart with ID {command.Id} not found");

            await ValidateProductsAsync(command.Products, existingCart.Products, cancellationToken);

            UpdateCartProducts(existingCart, command.Products);

            var updatedCart = await _cartRepository.UpdateAsync(existingCart, cancellationToken) ?? throw new KeyNotFoundException($"Cart with ID {command.Id} not found");

            return _mapper.Map<UpdateCartResult>(updatedCart);
        }

        private async Task ValidateProductsAsync(List<CartProductCommand> productsToUpdate, List<CartProduct> existingCartProducts, CancellationToken cancellationToken)
        {
            var existingProducts = await _productRepository.ListProductsByIdsAsync(productsToUpdate.Select(p => p.ProductId), false, cancellationToken) ?? throw new KeyNotFoundException($"No products found");

            if (existingProducts.Count() != productsToUpdate.Count)
            {
                var productsIds = productsToUpdate.Select(p => p.ProductId).ToList();
                var notFoundProducts = new List<Guid>();

                foreach (var productId in productsIds)
                {
                    if (existingProducts.Any(p => p.Id != productId))
                        notFoundProducts.Add(productId);
                }

                throw new KeyNotFoundException($"Products with id {string.Join("; ", productsIds)} not found");
            }
        }

        private static void UpdateCartProducts(Cart cart, List<CartProductCommand> productsToUpdate)
        {

            cart.Products.RemoveAll(p => !productsToUpdate.Any(ptu => ptu.ProductId == p.ProductId));


            foreach (var productCmd in productsToUpdate)
            {
                var existingProduct = cart.Products.FirstOrDefault(p => p.ProductId == productCmd.ProductId);

                if (existingProduct != null)
                {
                    existingProduct.Quantity = productCmd.Quantity;
                }
                else
                {
                    cart.Products.Add(new CartProduct
                    {
                        ProductId = productCmd.ProductId,
                        Quantity = productCmd.Quantity,
                        CartId = cart.Id
                    });
                }
            }
        }
    }
}
