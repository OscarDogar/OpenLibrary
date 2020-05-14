using System;
using System.Collections.Generic;
using System.Text;

namespace OpenLibrary.Common.Models
{
    public class ReviewResponse
    {
        public int Id { get; set; }

        public UserResponse User { get; set; }


        public string Comment { get; set; }

  
        public float Rating { get; set; }


        public bool Favorite { get; set; }


        public SearchResponse Document { get; set; }
    }
}
