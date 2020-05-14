using System;
using System.Collections.Generic;
using System.Text;

namespace OpenLibrary.Common.Models
{
    public class GenderResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<SearchResponse> Documents { get; set; }
    }
}
