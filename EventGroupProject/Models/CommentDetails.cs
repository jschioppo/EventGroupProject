using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventGroupProject.Models
{
    public class CommentDetails
    {
        public Comments Comment { get; set; }
        public int CommentUserId { get; set; }
        public int CommentEventId { get; set; }
    }
}
