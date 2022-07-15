using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OficinaSystem.Front.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
