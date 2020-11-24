using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using bd2_msft_project.Models;
using bd2_msft_project.Models.DBModels;
using bd2_msft_project.Utils.DB;
using Microsoft.AspNetCore.Http;
using bd2_msft_project.Utils;

namespace bd2_msft_project.Controllers
{
    public class HomeController : Controller
    {
        private static bool badLogin = false;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["IsLoggedIn"] = !string.IsNullOrEmpty(HttpContext.Session.GetString(nameof(UserData)));
            ViewData["BadLogin"] = badLogin;

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

        // POST: /Account/Login

        [HttpPost]
        public ActionResult Login(UserLogin model)
        {
            DataGetter dbLink = new DataGetter();
            UserData authenticatedUser = dbLink.GetUser(model.UserName, model.Password);

            if (authenticatedUser != null)
            {
                badLogin = false;
                HttpContext.Session.Set<UserData>(nameof(UserData), authenticatedUser);
            }
            else
            {
                badLogin = true;
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("Index", "Home");
        }
    }
}
