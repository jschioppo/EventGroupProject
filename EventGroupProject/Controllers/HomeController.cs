﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EventGroupProject.Models;
using Microsoft.AspNetCore.Identity;
using EventGroupProject.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace EventGroupProject.Controllers
{
    public class HomeController : Controller
    {
        private DBHandler _dbHandler { get; set; }

        public HomeController(DBHandler dbHandler)
        {
            _dbHandler = dbHandler;
        }

        public IActionResult Index()
        {
            if (!_dbHandler.UserTagsSelected())
            {
                List<Tag> tags = _dbHandler.GetAllTags();
                return View("TagSelection", tags);
            }
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public JsonResult SaveTags(string[] tagIds)
        {
            int userId = _dbHandler.GetUserId();

            _dbHandler.DeleteUserTags(userId);
            foreach(var id in tagIds)
            {
                int tagId = int.Parse(id);
                _dbHandler.AddTagToUser(userId, tagId);
            }
            _dbHandler.SetTagSelectedToTrue(userId);

            return Json(true);
        }
    }
}
