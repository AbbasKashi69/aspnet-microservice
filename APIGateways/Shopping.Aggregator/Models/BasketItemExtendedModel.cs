﻿namespace Shopping.Aggregator.Models
{
    public class BasketItemExtendedModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string? Color { get; set; }
        public decimal Price { get; set; }
        public string NPrice => Price.ToString("N0");
        public string Category { get; set; }

        public string  Summary { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
    }
}
