using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSaleProducts
{
    public class UpdateSaleProductsCommand : IRequest<UpdateSaleProductsResult>
    {
        public Guid SaleId { get; set; }
        public List<UpdateSaleProductItemRequest> Itens { get; set; } = [];
    }
}
