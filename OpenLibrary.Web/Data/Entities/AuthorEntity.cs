using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpenLibrary.Web.Data.Entities
{
    public class AuthorEntity
    {
        public int Id { get; set; }

        [MaxLength(80, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        public ICollection<DocumentEntity> Documents { get; set; }
    }
}
