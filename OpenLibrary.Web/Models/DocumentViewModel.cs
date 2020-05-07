using Microsoft.AspNetCore.Http;
using OpenLibrary.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace OpenLibrary.Web.Models
{
    public class DocumentViewModel : DocumentEntity
    {
        [Display(Name = "Document")]
        public IFormFile DocumentFile { get; set; }

    }
}
