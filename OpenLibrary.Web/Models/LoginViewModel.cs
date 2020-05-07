using System.ComponentModel.DataAnnotations;

namespace OpenLibrary.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "The field email is mandatory.")]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
