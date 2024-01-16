using Microsoft.AspNetCore.Mvc;

namespace Be.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
