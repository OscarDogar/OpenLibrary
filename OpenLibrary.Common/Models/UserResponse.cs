using OpenLibrary.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenLibrary.Common.Models
{
   public class UserResponse
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string DocumentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string PicturePath { get; set; }

        public LoginType LoginType { get; set; }

        public string PictureFullPath => string.IsNullOrEmpty(PicturePath)
        ? "https://openlibraryweb.azurewebsites.net//images/noimage.png"
        : LoginType == LoginType.OpenLibrary ? $"https://openlibraryweb.azurewebsites.net{PicturePath.Substring(1)}" : PicturePath;



        public UserType UserType { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<ReviewResponse> Reviews { get; set; }

        public ICollection<SearchResponse> Documents { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string FullNameWithDocument => $"{FirstName} {LastName} - {DocumentId}";
    }
}
