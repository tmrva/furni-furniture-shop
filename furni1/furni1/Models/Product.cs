using System.ComponentModel.DataAnnotations.Schema;

namespace furni1.Models
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public string Description { get; set; }
        public bool IsDeactive { get; set; }
        public string Image { get; set; }
        public List<ProductCategory>? ProductCategories { get; set; }

        [NotMapped]
		public IFormFile? Photo { get; set; }
	}
}
