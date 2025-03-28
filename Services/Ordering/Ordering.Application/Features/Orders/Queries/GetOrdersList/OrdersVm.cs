﻿

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    public class OrdersVm
    {
        public int Id { get; set; }
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
