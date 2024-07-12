using System.ComponentModel.DataAnnotations.Schema;

namespace furni1.Models
{
	public class Benefit
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Icon { get; set; }
	}
}
