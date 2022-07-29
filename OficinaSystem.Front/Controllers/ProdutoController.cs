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

        [Route("obterporid/{id}")]
        [HttpGet]
        public async Task<JsonResult> Obterporid(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    ConfigAPI api = new ConfigAPI();
                    string url = api.UrlAPI + "produto/" + id;
                    var response = await client.GetStringAsync(url);

                    var result = JsonConvert.DeserializeObject<ProdutoModel>(response);

                    return Json(result);
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        [Route("alterar")]
        [HttpPost]
        public JsonResult Alterar([FromBody] ProdutoModel produto)
        {

            ConfigAPI api = new ConfigAPI();
            string url = api.UrlAPI + "Produto/alterar";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(produto), Encoding.UTF8, "application/json")).Result;

                string json = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<int>(json);

                return Json(result);
            }
        }

        [Route("remover")]
        [HttpPost]
        public async Task<JsonResult> Remover([FromBody] int codigo)
        {
            ConfigAPI api = new ConfigAPI();
            string url = api.UrlAPI + "Produto/deletar";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(codigo), Encoding.UTF8, "application/json")).Result;

                string json = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<int>(json);

                return Json(result);
            }

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
