namespace Basket.API.Entities
{
    public class Cart
    {
        public string UserName { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public decimal TotalPrice => Items.Sum(s=> s.Price * s.Quantity);
        public string NTotalPrice => TotalPrice.ToString("N0");
    }
}
