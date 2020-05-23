using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OpenLibrary.Common.Models
{
    public class ReviewRequest
    {
        public string User { get; set; }

        public string Comment { get; set; }

        public float Rating { get; set; }

        public bool Favorite { get; set; }

        public int Document { get; set; }

        [Required]
        public string CultureInfo { get; set; }
    }
}
