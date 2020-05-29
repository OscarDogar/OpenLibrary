using Microsoft.AspNetCore.Identity;
using OpenLibrary.Common.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpenLibrary.Web.Data.Entities
{
    public class UserEntity : IdentityUser
    {
        [Display(Name = "Document")]
        [MaxLength(20, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string DocumentId { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string LastName { get; set; }

        [MaxLength(100, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        public string Address { get; set; }

        [Display(Name = "Login Type")]
        public LoginType LoginType { get; set; }


        [Display(Name = "Picture")]
        public string PicturePath { get; set; }

        [Display(Name = "User Type")]
        public UserType UserType { get; set; }

        public ICollection<DocumentEntity> Documents { get; set; }

        public ICollection<ReviewEntity> Reviews { get; set; }

        [Display(Name = "Nombre")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "Nombre y Documento")]
        public string FullNameWithDocument => $"{FirstName} {LastName} - {DocumentId}";
    }

}
