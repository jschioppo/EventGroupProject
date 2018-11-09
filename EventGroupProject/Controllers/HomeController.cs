using System;
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
using Microsoft.AspNetCore.Authorization;

namespace EventGroupProject.Controllers
{
    public class HomeController : Controller
    {
        private DBHandler _dbHandler { get; set; }
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(DBHandler dbHandler, SignInManager<ApplicationUser> signInManager)
        {
            _dbHandler = dbHandler;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            if (_dbHandler.UserIsBanned())
            {
                _signInManager.SignOutAsync();
                return View("Banned");
            }

            bool isAuthenticated = User.Identity.IsAuthenticated; 
            if (isAuthenticated && !_dbHandler.UserTagsSelected())
            {
                List<Tag> tags = _dbHandler.GetAllTags();
                return View("TagSelection", tags);
            }
            return View(_dbHandler.GetUserEvents());
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ViewAllUsers()
        {
            if (_dbHandler.IsAdmin(_dbHandler.GetUserId()))
            {
                return View(_dbHandler.GetAllUsers());
            }

            return View("Index", _dbHandler.GetUserEvents());
        }

        [HttpPost]
        public JsonResult SaveTags(string[] tagIds, string city)
        {
            int userId = _dbHandler.GetUserId();

            _dbHandler.DeleteUserTags(userId);
            foreach(var id in tagIds)
            {
                int tagId = int.Parse(id);
                _dbHandler.AddTagToUser(userId, tagId);
            }
            _dbHandler.SetTagSelectedToTrue(userId);
            _dbHandler.AddCityToUser(city);

            return Json(true);
        }

        [HttpGet]
        public JsonResult IsUserAdmin()
        {
            return Json(_dbHandler.IsAdmin(_dbHandler.GetUserId()));
        }

        [HttpPost]
        public void BanUser(int userId)
        {
            _dbHandler.BanUser(userId);
        }

        [HttpPost]
        public void MakeUserAdmin(int userId)
        {
            _dbHandler.MakeUserAdmin(userId);
        }
    }
}
