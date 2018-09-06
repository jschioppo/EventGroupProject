using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EventGroupProject.Controllers
{
    public class EventFeedController : Controller
    {
        public IActionResult EventFeed()
        {
            return View();
        }
    }
}