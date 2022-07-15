using Microsoft.AspNetCore.Mvc;

namespace OficinaSystem.Front.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
