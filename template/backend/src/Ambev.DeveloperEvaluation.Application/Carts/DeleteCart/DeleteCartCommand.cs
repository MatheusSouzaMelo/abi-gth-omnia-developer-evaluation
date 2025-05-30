using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

public record DeleteCartCommand : IRequest<DeleteCartResponse>
{
    public Guid Id { get; set; }
}
