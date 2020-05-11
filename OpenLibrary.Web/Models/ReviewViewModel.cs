using OpenLibrary.Web.Data.Entities;
using System;

namespace OpenLibrary.Web.Models
{
    public class ReviewViewModel : ReviewEntity
    {
        public int DocumentId { get; set; }
    }
}
