using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventGroupProject.Models
{
    public class Events
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public string Location { get; set; }
        public int Price { get; set; }
        public User EventCreator { get; set; } 
        public List<Tag> EventTags { get; set; }
        public List<User> SignedUpUsers { get; set; } 
    }
}
