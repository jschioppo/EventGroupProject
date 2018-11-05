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
        public IActionResult Index(int eventId)
        {
            return View(_dbHandler.GetEvent(eventId));
        }

        public ActionResult _EventComments(int eventId)
        {
            return PartialView(_dbHandler.GetComments(eventId));
        }

        [HttpGet]
        public JsonResult GetUserId()
        {
            return Json(_dbHandler.GetUserId());
        }

        [HttpGet]
        public JsonResult IsUserSignedUp(int eventId)
        {
            int userId = _dbHandler.GetUserId();
            return Json(_dbHandler.IsUserRegisteredToEvent(userId, eventId) || _dbHandler.IsAdmin(userId));
        }

        [HttpGet]
        public JsonResult IsUserAdmin()
        {
            return Json(_dbHandler.IsAdmin(_dbHandler.GetUserId()));
        }

        [HttpGet]
        public JsonResult IsUserEventCreator(int eventId)
        {
            return Json(_dbHandler.IsEventCreator(_dbHandler.GetUserId(), eventId));
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

        [HttpPost]
        public JsonResult AddUserToEvent(int eventId)
        {
            return Json(_dbHandler.AddUserToEvent(eventId)); //Return Json?

        }

    }
}
