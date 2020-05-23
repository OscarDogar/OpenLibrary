using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OpenLibrary.Common.Models
{
    public class ReviewDeleteRequest
    {
        public string User { get; set; }

        public int Document { get; set; }

        [Required]
        public string CultureInfo { get; set; }
    }
}
