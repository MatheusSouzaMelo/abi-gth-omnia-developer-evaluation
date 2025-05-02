using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales
{
    public class ListSaleResponse
    {
        public IQueryable<GetSaleResponse> Sales { get; set; }
    }
}
