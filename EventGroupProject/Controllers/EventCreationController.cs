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
        public JsonResult saveEventData(string eventName, string eventCity, string dateTime, int duration, 
            string address, int cost, string description, string[] tagIds)
        {
            return Json(true);
        }
    }
}