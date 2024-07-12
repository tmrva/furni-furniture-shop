using System;
using System.Collections.Generic;

namespace furni1.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string StateCountry { get; set; }
        public string PostalZip { get; set; }
        public string OrderNotes { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public Order()
        {
            OrderDate = DateTime.UtcNow.AddHours(4);
        }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
