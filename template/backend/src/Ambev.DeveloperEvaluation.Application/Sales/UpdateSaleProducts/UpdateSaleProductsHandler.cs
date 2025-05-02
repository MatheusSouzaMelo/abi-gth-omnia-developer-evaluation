using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSaleProducts
{
    public class UpdateSaleProductsHandler : IRequestHandler<UpdateSaleProductsCommand, UpdateSaleProductsResult>
    {
        private readonly ISaleRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateSaleProductsHandler(ISaleRepository repository, IMapper mapper, IProductRepository productRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<UpdateSaleProductsResult> Handle(UpdateSaleProductsCommand command, CancellationToken cancellationToken)
        {
            var updateSaleProductCommandValidator = new UpdateSaleProductsCommandValidator();
            var validationResult = updateSaleProductCommandValidator.Validate(command);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _repository.GetSaleByIdWithAllDependenciesAsync(command.SaleId, tracking: true, cancellationToken) ?? throw new KeyNotFoundException($"Sale with id {command.SaleId} not found");

            await UpdateSaleProduct(sale, command);

            UpdateSaleTotals(sale);

            var updatedSale = await _repository.UpdateAsync(sale, cancellationToken) ?? throw new Exception($"Error updating sale {sale.Id}");

            Console.WriteLine($"channel.BasicPublish(exchange: 'exchange', routingKey: 'Sales', 'body: Encoding.UTF8.GetBytes('{JsonSerializer.Serialize(updatedSale)}'))");

            return _mapper.Map<UpdateSaleProductsResult>(updatedSale);
        }

        private async Task UpdateSaleProduct(Sale sale, UpdateSaleProductsCommand command)
        {
            foreach (var item in command.Itens)
            {
                var saleProduct = sale.Products.FirstOrDefault(p => p.CartProduct.ProductId == item.ProductId);
                if (saleProduct != null)
                {
                    saleProduct.CartProduct.Quantity = item.Quantity;
                    saleProduct.Canceled = item.Canceled;

                    saleProduct.CalculateValues();
                }
                else
                {
                    await CreateSaleProductAsync(item, sale);
                }
            }
        }

        private async Task CreateSaleProductAsync(UpdateSaleProductItemRequest updateSaleProduct, Sale sale)
        {
            var product = await _productRepository.GetByIdAsync(updateSaleProduct.ProductId) ?? throw new KeyNotFoundException($"product with id {updateSaleProduct.ProductId} not found");

            var cartProduct = new CartProduct
            {
                CartId = sale.CartId,
                ProductId = product.Id,
                Quantity = updateSaleProduct.Quantity,
                Product = product
            };

            var saleProduct = new SaleProduct
            {
                CartProduct = cartProduct,
                UnitPrice = product.Price,
                Canceled = updateSaleProduct.Canceled,
                SaleId = sale.Id                
            };

            saleProduct.CalculateValues();

            sale.Products.Add(saleProduct);
        }
        private static void UpdateSaleTotals(Sale sale)
        {
            sale.TotalAmount = sale.Products
                .Where(p => !p.Canceled)
                .Sum(p => p.TotalAmount);
        }
    }
}
