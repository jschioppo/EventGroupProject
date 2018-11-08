using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventGroupProject.Models
{
    public class EventsRecommendedModel
    {
        public EventsRecommendedModel()
        {
            Tags = new List<Tag>();
        }
        public List<Tag> Tags { get; set; }
        public string Location { get; set; }
    }

}
