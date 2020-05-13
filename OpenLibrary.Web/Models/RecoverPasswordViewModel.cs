using System.ComponentModel.DataAnnotations;

namespace OpenLibrary.Web.Models
{
    public class RecoverPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
