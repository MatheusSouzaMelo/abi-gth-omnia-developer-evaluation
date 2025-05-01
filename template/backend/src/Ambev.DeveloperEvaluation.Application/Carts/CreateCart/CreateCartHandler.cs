using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

public class CreateCartHandler : IRequestHandler<CreateCartCommand, CreateCartResult>
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreateCartHandler(ICartRepository CartRepository, IMapper mapper, IProductRepository productRepository)
    {
        _cartRepository = CartRepository;
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<CreateCartResult> Handle(CreateCartCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateCartCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var (areAllProductsValid, message) = await ValidateCartProductsHelper.ValidateCartProductsAsync(_productRepository, command.Products, cancellationToken);

        if (!areAllProductsValid)
        {
            throw new ValidationException(message);
        }

        var Cart = _mapper.Map<Cart>(command);

        var createdCart = await _cartRepository.CreateAsync(Cart, cancellationToken);
        var result = _mapper.Map<CreateCartResult>(createdCart);
        return result;
    }
}
