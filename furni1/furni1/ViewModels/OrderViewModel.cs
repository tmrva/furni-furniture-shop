using System.Collections.Generic;

namespace furni1.Models
{
    public class OrderViewModel
    {
        public List<Product> Products { get; set; }
        public Dictionary<int, int> Quantities { get; set; }
        public decimal CartTotal { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string StateCountry { get; set; }
        public string PostalZip { get; set; }
        public string OrderNotes { get; set; }
    }
}
