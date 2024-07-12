using System.ComponentModel.DataAnnotations;

namespace furni1.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "You have to enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You have to enter your surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "You have to enter your username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "You have to enter your email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You have to enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "You have to repeat your password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "You have to enter the same password")]
        public string RepeatPassword { get; set; }
        public bool IsRemember { get; set; }
    }
}
