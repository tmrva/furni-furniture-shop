using System.ComponentModel.DataAnnotations;

namespace furni1.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "You have to enter your username or email")]
        public string Username { get; set; }

        [Required(ErrorMessage = "You have to enter your email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You have to enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRemember { get; set; }
    }
}
