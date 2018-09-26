using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventGroupProject.Models
{
    public class SearchResult
    {
        public string City { get; set; }
        public List<Tag> Tags { get; set; }
        public List<Events> ResultEvents { get; set; }

    }
}
