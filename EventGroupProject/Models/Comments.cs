using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventGroupProject.Models
{
    public class Comments
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public string Content { get; set; }
        public string UserDisplayName{ get; set; }
    }
}
