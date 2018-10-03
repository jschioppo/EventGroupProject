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
    }
}