using System.ComponentModel.DataAnnotations;

namespace furni1.ViewModels
{
    public class ResetPasswordVM
    {
        [Required(ErrorMessage = "You have to enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "You have to repeat your password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "You have to enter the same password")]
        public string RepeatPassword { get; set; }
    }
}
