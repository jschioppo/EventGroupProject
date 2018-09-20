using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventGroupProject.Models
{
    public class Events
    {
        public char City { get; set; }
        public char Description { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public char Location { get; set; }
        public int Price { get; set; }
    }
}
