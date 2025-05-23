﻿using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartCommand : IRequest<UpdateCartResult>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public List<CartProductCommand> Products { get; set; } = [];
    }
}
