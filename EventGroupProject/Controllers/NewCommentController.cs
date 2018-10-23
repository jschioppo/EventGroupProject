using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EventGroupProject.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventGroupProject.Controllers
{
    public class NewCommentController : Controller
    {
        DBHandler _dbHandler;

        public NewCommentController(DBHandler dBHandler)
        {
            _dbHandler = dBHandler;

        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveCommentData(string commentBody, int eventId) //int userId, int eventId
        {
            //int writerId = _dbHandler.GetUserId();
            int eventID = eventId;

            bool saveData = _dbHandler.AddComment(commentBody, eventId);
            if (!saveData)
            {
                return Json(false);
            }

            return Json(true);
        }
    }
    
}
