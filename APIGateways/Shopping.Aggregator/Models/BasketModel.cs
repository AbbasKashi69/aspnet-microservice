namespace Shopping.Aggregator.Models
{
    public class BasketModel
    {
        public string UserName { get; set; }
        public List<BasketItemExtendedModel> Items { get; set; } = new List<BasketItemExtendedModel>();
        public decimal TotalPrice => Items.Sum(s => s.Price * s.Quantity);
        public string NTotalPrice => TotalPrice.ToString("N0");
    }
}
