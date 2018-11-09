using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventGroupProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EventGroupProject.Controllers
{
    [Authorize]
    public class EventsRecommendedSearching : Controller
    {
        private DBHandler _dbHandler { get; set; }

        public EventsRecommendedSearching(DBHandler dbHandler)
        {
            _dbHandler = dbHandler;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(new EventsRecommendedModel());
        }

        public ActionResult _Search()
        {
            return PartialView(_dbHandler.GetAllTags());
        }

        public ActionResult _ResultView(string[] tagIds, string city)
        {
            return PartialView(_dbHandler.SearchEvents(new List<int>(Array.ConvertAll(tagIds, int.Parse)), city));
        }

    }
}
