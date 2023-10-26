using Microsoft.AspNetCore.Mvc;

namespace Blog_WebSite.Controllers
{
    public class PageController : Controller
    {
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult PrivacyP()
        {
            return View();
        }
    }
}
