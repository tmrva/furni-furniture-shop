using System.ComponentModel.DataAnnotations.Schema;

namespace furni1.Models
{
	public class Testimonial
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public string Name { get; set; }
		public string Profession { get; set; }
        public bool IsDeactive { get; set; }
        public string Image { get; set; }

		[NotMapped]
		public IFormFile? Photo { get; set; }
	}
}
