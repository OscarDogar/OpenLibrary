using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using OpenLibrary.Web.Data.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpenLibrary.Web.Models
{
    public class DocumentViewModel : DocumentEntity
    {
        [Display(Name = "Document")]
        public IFormFile DocumentFile { get; set; }


        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Gender")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Gender.")]
        public int GenderId { get; set; }

        public IEnumerable<SelectListItem> Genders { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Author")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select an Author.")]
        public int AuthorId { get; set; }

        public IEnumerable<SelectListItem> Authors { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Type Of Document")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Type Of Document.")]
        public int TypeOfDocumentId { get; set; }

        public IEnumerable<SelectListItem> TypeOfDocuments { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Document Language")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select languages for the document.")]
        public int DocumentLanguageId { get; set; }

        public IEnumerable<SelectListItem> DocumentLanguages { get; set; }
    }
}
