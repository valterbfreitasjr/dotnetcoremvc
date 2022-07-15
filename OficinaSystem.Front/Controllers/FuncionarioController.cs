using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OficinaSystem.Front.Models;

namespace OficinaSystem.Front.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : Controller
    {
        [Route("obtertodos")]
        [HttpGet]
        public async Task<IEnumerable<Funcionario>> ObterTodos()
        {

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    ConfigAPI api = new ConfigAPI();
                    string url = api.UrlAPI + "Funcionario/obtertodos";
                    var response = await client.GetStringAsync(url);
                    var convenios = JsonConvert.DeserializeObject<IEnumerable<Funcionario>>(response);
                    return convenios;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
