using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OficinaSystem.Front.Models;
using System.Text;

namespace OficinaSystem.Front.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : Controller
    {
        [Route("obtertodos")]
        [HttpGet]
        public async Task<IEnumerable<ProdutoModel>> ObterTodos()
        {

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    ConfigAPI api = new ConfigAPI();
                    string url = api.UrlAPI + "Produto/obtertodos";
                    var response = await client.GetStringAsync(url);
                    var convenios = JsonConvert.DeserializeObject<IEnumerable<ProdutoModel>>(response);
                    return convenios;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

        }

        [Route("adicionar")]
        [HttpPost]
        public JsonResult Adicionar([FromBody] ProdutoModel produto)
        {
            ConfigAPI api = new ConfigAPI();
            string url = api.UrlAPI + "Produto/adicionar";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(produto), Encoding.UTF8, "application/json")).Result;

                string json = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ProdutoModel>(json);

                return Json(result);
            }

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
