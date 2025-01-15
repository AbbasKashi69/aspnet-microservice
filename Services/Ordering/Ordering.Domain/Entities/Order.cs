

namespace Ordering.Domain.Entities
{
    public class Order : EntityBase
    {
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string City { get; set; }

        public string BankName { get; set; }
        public int PaymentMethod { get; set; }


    }
}
