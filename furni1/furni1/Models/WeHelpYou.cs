using System.ComponentModel.DataAnnotations.Schema;

namespace furni1.Models
{
	public class WeHelpYou
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string DescriptionLeft { get; set; }
		public string DescriptionRigth { get; set; }
		public string Image1 { get; set; }
		public string Image2 { get; set; }
		public string Image3 { get; set; }

        [NotMapped]
        public IFormFile? Photo1 { get; set; }
        [NotMapped]
        public IFormFile? Photo2 { get; set; }
        [NotMapped]
        public IFormFile? Photo3 { get; set; }
    }
}
