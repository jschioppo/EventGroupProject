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

namespace EventGroupProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private string AuthenticatedUser;
        DBHandler handler;

        public HomeController(
            UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            AuthenticatedUser = contextAccessor.HttpContext.User.Identity.Name;
            handler = new DBHandler();
        }


        public IActionResult Index()
        {
            if (!handler.UserTagsSelected(AuthenticatedUser))
            {
                return View("TagSelection", this);
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
    }
}
