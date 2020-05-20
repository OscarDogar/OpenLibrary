using System;
using System.Collections.Generic;
using System.Text;

namespace OpenLibrary.Common.Models
{
    public class SearchResponse
    {
        public int Id { get; set; }


        public string Title { get; set; }


        public string DocumentPath { get; set; }

        public string DocumentFullPath => string.IsNullOrEmpty(DocumentPath)
            ? "https://openlibraryweb.azurewebsites.net/images/noimage.png"
            : $"https://openlibraryweb.azurewebsites.net{DocumentPath.Substring(1)}";

        public DateTime Date { get; set; }

 
        public DateTime DateLocal => Date.ToLocalTime();

        public string Summary { get; set; }

        public int PagesNumber { get; set; }

        public bool Accepted { get; set; }

        public UserResponse User { get; set; }

        public GenderResponse Gender { get; set; }

        public AuthorResponse Author { get; set; }

        public DocumentLanguageResponse DocumentLanguage { get; set; }

        public TypeOfDocumentResponse TypeOfDocument { get; set; }

        public ICollection<ReviewResponse> Reviews { get; set; }
    }
}
