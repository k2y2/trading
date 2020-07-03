using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using trading.Models;

namespace trading.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; 
        private readonly tradingContext _context = new tradingContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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

        [HttpPost]
        public ActionResult Login(string UserName, string Password)
        { 
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password)) return View("Index");

            var user = _context.User.FirstOrDefault(m => m.UserName == UserName && m.Password == Password);

            if (user != null)
            {
                HttpContext.Session.SetString("UserName", UserName);
                return RedirectToAction("Index", "Txn"); 
            }
            else
            {
                ViewBag.error = "Invalid Account";
                return View("Index");
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.Session.Remove("UserName");
            return View("Index");
        }
    }
}
