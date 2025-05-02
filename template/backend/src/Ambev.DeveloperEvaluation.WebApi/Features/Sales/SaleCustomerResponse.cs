namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    public class SaleCustomerResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
