using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    public class UserForAuthenticationModel
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}