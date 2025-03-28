﻿namespace Basket.API.Entities
{
    public class CartItem
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string? Color { get; set; }
        public decimal Price { get; set; }
        public string NPrice => Price.ToString("N0");
    }
}
