using Ambev.DeveloperEvaluation.Application.Common;
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

            var (areAllProductsValid, message) = await ValidateCartProductsHelper.ValidateCartProductsAsync(_productRepository, command.Products, cancellationToken);

            if (!areAllProductsValid)
            {
                throw new ValidationException(message);
            }

            UpdateCartProducts(existingCart, command.Products);

            var updatedCart = await _cartRepository.UpdateAsync(existingCart, cancellationToken) ?? throw new KeyNotFoundException($"Cart with ID {command.Id} not found");

            return _mapper.Map<UpdateCartResult>(updatedCart);
        }

        private void UpdateCartProducts(Cart cart, List<CartProductCommand> productsToUpdate)
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
                    var cartProduct = _mapper.Map<CartProduct>(productCmd);
                    cart.Products.Add(cartProduct);
                }
            }
        }
    }
}
