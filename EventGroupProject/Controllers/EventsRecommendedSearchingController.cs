using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventGroupProject.Models;
using Microsoft.AspNetCore.Mvc;


namespace EventGroupProject.Controllers
{
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
            return View();
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
