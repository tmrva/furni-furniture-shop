using System.ComponentModel.DataAnnotations;

namespace furni1.ViewModels
{
    public class EditVM
    {
        [Required(ErrorMessage = "You have to enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You have to enter your surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "You have to enter your username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "You have to enter your email")]
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
