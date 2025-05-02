using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        public Guid CartId { get; set; }
        public string Branch { get; set; } = string.Empty;
    }
}
