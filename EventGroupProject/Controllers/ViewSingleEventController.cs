using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventGroupProject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventGroupProject.Controllers
{
    public class ViewSingleEvent : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            Events newEvent = new Events
            {
                EventName = "Test",
                EventId = 1
            };

            return View(newEvent);
        }

        [HttpGet]
        public JsonResult GetComments(int eventId)
        {
            List<Comments> comments = new List<Comments>()
            {
                new Comments{CommentId = 1, Content = "Comment 1"},
                new Comments{CommentId = 2, Content = "Comment 2"},
                new Comments{CommentId = 3, Content = "Comment 3"}
            };

            return Json(comments);
        }

        
    }
}
