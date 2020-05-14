using System;
using System.Collections.Generic;
using System.Text;

namespace OpenLibrary.Common.Models
{
    public class AuthorResponse
    {
        public int Id { get; set; }


        public string Name { get; set; }

        public ICollection<SearchResponse> Documents { get; set; }
    }
}
