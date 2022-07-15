using Microsoft.AspNetCore.Mvc;

namespace OficinaSystem.Front.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
