using System.ComponentModel.DataAnnotations;

namespace furni1.Models
{
	public class Message
	{
        public int Id { get; set; }
        [Required(ErrorMessage ="You have to enter your email")]
        public string Email { get; set; }
		[Required(ErrorMessage = "You have to enter the subject of message")]
		public string Subject { get; set; }
		[Required(ErrorMessage = "You have to enter your message")]
		public string Text { get; set; }
		public bool IsReplied { get; set; }
    }
}
