using System.ComponentModel.DataAnnotations.Schema;

namespace furni1.Models
{
	public class Blog
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string By { get; set; }
		public DateTime CreatedTime { get; set; }
		public Blog()
		{
			CreatedTime = DateTime.UtcNow.AddHours(4);
		}
		public string Description { get; set; }
		public string Image { get; set; }
        public bool IsDeactive { get; set; }

        [NotMapped]
		public IFormFile? Photo { get; set; }
	}
}
