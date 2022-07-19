using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OficinaSystem.Front.Models;
using System.Text;

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


        [Route("adicionar")]
        [HttpPost]
        public JsonResult Adicionar([FromBody] Funcionario funcionario)
        {
            ConfigAPI api = new ConfigAPI();
            string url = api.UrlAPI + "Funcionario/adicionar";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(funcionario), Encoding.UTF8, "application/json")).Result;

                string json = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<Funcionario>(json);

                return Json(result);
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
                    string url = api.UrlAPI + "funcionario/" + id;
                    var response = await client.GetStringAsync(url);

                    var result = JsonConvert.DeserializeObject<Funcionario>(response);

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
        public JsonResult Alterar([FromBody] Funcionario funcionario)
        {

            ConfigAPI api = new ConfigAPI();
            string url = api.UrlAPI + "Funcionario/alterar";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(funcionario), Encoding.UTF8, "application/json")).Result;

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
            string url = api.UrlAPI + "Funcionario/delete";
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
