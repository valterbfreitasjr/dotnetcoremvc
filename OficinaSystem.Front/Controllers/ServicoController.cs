using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OficinaSystem.Front.Models;
using System.Text;

namespace OficinaSystem.Front.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicoController : Controller
    {
        [Route("adicionar")]
        [HttpPost]
        public JsonResult Adicionar([FromBody] ServicoModel servico)
        {
            ConfigAPI api = new ConfigAPI();
            string url = api.UrlAPI + "Servico/adicionar";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(servico), Encoding.UTF8, "application/json")).Result;

                string json = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ServicoModel>(json);

                return Json(result);
            }

        }

        [Route("obtertodos")]
        [HttpGet]
        public async Task<IEnumerable<ServicoModel>> ObterTodos()
        {

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    ConfigAPI api = new ConfigAPI();
                    string url = api.UrlAPI + "Servico/obtertodos";
                    var response = await client.GetStringAsync(url);
                    var convenios = JsonConvert.DeserializeObject<IEnumerable<ServicoModel>>(response);
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
                    string url = api.UrlAPI + "Servico/" + id;
                    var response = await client.GetStringAsync(url);

                    var result = JsonConvert.DeserializeObject<ServicoModel>(response);

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
        public JsonResult Alterar([FromBody] ServicoModel servico)
        {

            ConfigAPI api = new ConfigAPI();
            string url = api.UrlAPI + "Servico/alterar";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(servico), Encoding.UTF8, "application/json")).Result;

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
            string url = api.UrlAPI + "Servico/deletar";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(codigo), Encoding.UTF8, "application/json")).Result;

                string json = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<int>(json);

                return Json(result);
            }

        }

        [Route("obterservicospedido/{id}")]
        [HttpGet]
        public async Task<JsonResult> GetServicoPedido(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    ConfigAPI api = new ConfigAPI();
                    string url = api.UrlAPI + "servico/obterservicospedido/" + id;
                    var response = await client.GetStringAsync(url);

                    var result = JsonConvert.DeserializeObject<List<ServicoModel>>(response);

                    return Json(result);
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
