using System.ComponentModel.DataAnnotations.Schema;

namespace furni1.Models
{
	public class Bio
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }

		[NotMapped]
		public IFormFile? Photo { get; set; }
	}
}
