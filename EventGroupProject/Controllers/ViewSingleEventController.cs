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
    public class ViewSingleEventController : Controller
    {
        private DBHandler _dbHandler { get; set; }

        public ViewSingleEventController(DBHandler dbHandler)
        {
            _dbHandler = dbHandler;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_dbHandler.GetEvent(5));
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
