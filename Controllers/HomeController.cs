using Microsoft.AspNetCore.Mvc;

namespace YourApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // handles unhandled exceptions in production
        public IActionResult Error()
        {
            return View();
        }
    }
}
