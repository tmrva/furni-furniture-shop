using furni1.Models;

namespace furni1.ViewModels
{
	public class ProductVM
	{
        public List<Product> Products { get; set; }
		public int ProductCount { get; set; }
        public List<Category> Categories { get; set; }
	}
}
