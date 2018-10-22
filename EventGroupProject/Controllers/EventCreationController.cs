using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EventGroupProject.Models;

namespace EventGroupProject.Controllers
{
    public class EventCreationController : Controller
    {
        DBHandler _dbHandler;

        public EventCreationController(DBHandler dBHandler)
        {
            _dbHandler = dBHandler;
            
        }

        public IActionResult Index()
        {
            List<Tag> tags = _dbHandler.GetAllTags();
            return View(tags);
        }
        [HttpPost]
        public JsonResult SaveEventData(string eventName, string eventCity, string dateTime, int duration, 
            string address, int cost, string description, string[] tagIds)
        {
            DateTime startDateTime = Convert.ToDateTime(dateTime);
            int creatorId = _dbHandler.GetUserId();
            int newEventId = _dbHandler.AddEvent(eventName, eventCity, description, startDateTime, duration, address, cost, creatorId);

            foreach (var id in tagIds)
            {
                int tagId = int.Parse(id);
                _dbHandler.AddTagToEvent(newEventId, tagId);
            }

            return Json(true);
            //TODO: Add stuff to eventtotag
        }
    }
}