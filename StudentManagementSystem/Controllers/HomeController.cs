using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StudentManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace StudentManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration config)
        {
            this._configuration = config;
        }

        public IActionResult Index()
        {
            HttpContext.Session.SetString("Test", "Session Value");
            return View();
        }
        public IActionResult Dashboard()
        {
            HttpContext.Session.SetString("Test", "Session Value");
            return View();
        }

        public IActionResult Login()
        {
            ViewBag.Session=HttpContext.Session.GetString("Test");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
