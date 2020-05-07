using System;
using System.ComponentModel.DataAnnotations;

namespace OpenLibrary.Web.Data.Entities
{
    public class DocumentEntity
    {
        public int Id { get; set; }

        [Display(Name = "Titulo")]
        [MaxLength(80, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Title { get; set; }

        [Display(Name = "Document")]
        public string DocumentPath { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime Date { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime DateLocal => Date.ToLocalTime();

        [Display(Name = "Summary")]
        [MaxLength(300, ErrorMessage = "The {0} field can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Summary { get; set; }

        [Display(Name = "Pages Number")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public int PagesNumber { get; set; }
        
        //public int Accepted { get; set; }

        [Display(Name = "Gender")]
        public GenderEntity Gender { get; set; }

        [Display(Name = "Author")]
        public AuthorEntity Author { get; set; }

        [Display(Name = "Docuemnt Language")]
        public DocumentLanguageEntity DocumentLanguage { get; set; }

        [Display(Name = "Type Of Document")]
        public TypeOfDocumentEntity TypeOfDocument { get; set; }
    }
}
