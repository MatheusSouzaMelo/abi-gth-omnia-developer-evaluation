using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Channels;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICartProductRepository _cartProductRepository;
        private readonly IMapper _mapper;

        public CreateSaleHandler(
            ISaleRepository saleRepository,
            ICartRepository cartRepository,
            IUserRepository userRepository,
            ICartProductRepository cartProductRepository,
            IMapper mapper)
        {
            _saleRepository = saleRepository;
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _cartProductRepository = cartProductRepository;
            _mapper = mapper;
        }
        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleValidator();
            var validationResult = validator.Validate(command);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingSale = await _saleRepository.GetSaleByCartId(command.CartId);

            if (existingSale != null)
                throw new Exception("Sale already made");

            var cart = await _cartRepository.GetByIdAsync(command.CartId, cancellationToken, true, c => c.Products) ?? throw new KeyNotFoundException("Cart not found");

            var cartProducts = await _cartProductRepository.ListCartProductsByIds(cart.Products.Select(p => p.Id),
                cancellationToken: cancellationToken, includes: c => c.Product) ?? throw new KeyNotFoundException("Products not found");

            if (cart.Products.Count != cartProducts.Count())
            {
                var nonExistingProducts = new List<Guid>();

                foreach (var cartProduct in cart.Products)
                {
                    var product = cartProducts.FirstOrDefault(p => p.Id == cartProduct.ProductId);

                    if (product == null)
                        nonExistingProducts.Add(cartProduct.ProductId);
                }

                throw new KeyNotFoundException($"Products {string.Join(", ", nonExistingProducts)} are not avaliable");
            }

            cart.Products = cartProducts.ToList();

            var customer = await _userRepository.GetByIdAsync(cart.UserId, cancellationToken) ?? throw new KeyNotFoundException("Customer not found");

            var sale = new Sale
            {
                SaleNumber = GenerateSaleNumber(),
                SaleDate = DateTime.UtcNow,
                CustomerId = customer.Id,
                Branch = command.Branch,
                CartId = cart.Id,
                Products = cart.Products.Select(cp => new SaleProduct
                {
                    CartProductId = cp.Id,
                    UnitPrice = cp.Product.Price,
                    CartProduct = cp,
                }).ToList()
            };

            CalculateProductPrices(sale.Products);

            sale.TotalAmount = sale.Products.Sum(p => p.TotalAmount);

            var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

            var result = _mapper.Map<CreateSaleResult>(createdSale);
            result.Customer = _mapper.Map<SaleCustormerResult>(customer);

            Console.WriteLine($"channel.BasicPublish(exchange: 'exchange', routingKey: 'Sales', 'body: Encoding.UTF8.GetBytes('{JsonSerializer.Serialize(createdSale)}'))");

            return result;
        }

        private static string GenerateSaleNumber()
        {
            return $"SALE-{Guid.NewGuid()}";
        }

        private static void CalculateProductPrices(List<SaleProduct> products)
        {
            foreach (var product in products)
            {
                product.CalculateValues();
            }
        }
    }
}
