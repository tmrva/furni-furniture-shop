using System.ComponentModel.DataAnnotations.Schema;

namespace furni1.Models
{
	public class WhyChooseUs
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }
        public string MiniTitle1 { get; set; }
        public string MiniDescription1 { get; set; }
        public string MiniIcon1 { get; set; }
        public string MiniTitle2 { get; set; }
        public string MiniDescription2 { get; set; }
        public string MiniIcon2 { get; set; }
        public string MiniTitle3 { get; set; }
        public string MiniDescription3 { get; set; }
        public string MiniIcon3 { get; set; }
        public string MiniTitle4 { get; set; }
        public string MiniDescription4 { get; set; }
        public string MiniIcon4 { get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }
        [NotMapped]
        public IFormFile? Photo1 { get; set; }
        [NotMapped]
        public IFormFile? Photo2 { get; set; }
        [NotMapped]
        public IFormFile? Photo3 { get; set; }
        [NotMapped]
        public IFormFile? Photo4 { get; set; }
    }
}
