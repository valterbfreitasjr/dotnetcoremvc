using Microsoft.AspNetCore.Mvc;

namespace OficinaSystem.Front.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}