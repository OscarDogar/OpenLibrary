using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenLibrary.Web.Data.Entities
{
    public class ReviewEntity
    {
        public int Id { get; set; }

        [MaxLength(200, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Comment { get; set; }

        [Display(Name = "Rating")]
        public float Rating { get; set; }

        [Display(Name = "Favorite ?")]
        public bool Favorite { get; set; }

        public UserEntity User { get; set; }

        public DocumentEntity Document { get; set; }
    }
}
