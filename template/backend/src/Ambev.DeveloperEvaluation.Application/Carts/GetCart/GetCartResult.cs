﻿namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart
{
    public class GetCartResult
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public List<CartProductResult> Products { get; set; } = [];
    }
}
