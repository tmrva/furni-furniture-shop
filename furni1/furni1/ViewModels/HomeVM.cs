using furni1.Models;

namespace furni1.ViewModels
{
	public class HomeVM
	{
		public ContentHome ContentHome { get; set; }
		public ContentProduct ContentProduct { get; set; }
		public WhyChooseUs WhyChooseUs { get; set; }
		public WeHelpYou WeHelpYou { get; set; }
		public List<Product> Products { get; set; }
		public List<TeamMember> TeamMembers { get; set; }
		public List<Blog> Blogs { get; set; }
		public List<Testimonial> Testimonials { get; set; }
	}
}
