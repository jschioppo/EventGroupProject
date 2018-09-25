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
        //TODO: Needs to be uncommented after Event model is added
        //public List<Event> ResultEvents { get; set; }

    }
}
