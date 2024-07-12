using System.Collections.Generic;

namespace furni1.Models
{
    public class CartViewModel
    {
        public List<Product> Products { get; set; }
        public Dictionary<int, int> Quantities { get; set; }
        public decimal CartTotal { get; set; }
    }
}
