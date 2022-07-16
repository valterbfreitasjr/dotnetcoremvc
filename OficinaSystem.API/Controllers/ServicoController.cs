using Microsoft.AspNetCore.Mvc;
using OficinaSystem.API.ViewModel;
using OficinaSystem.Domain.Entity;
using OficinaSystem.Domain.Interfaces;

namespace OficinaSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicoController : ControllerBase
    {
        private readonly IServicoRepositorie _servicoRepositorie;

        public ServicoController(IServicoRepositorie servicoRepositorie)
        {
            _servicoRepositorie = servicoRepositorie;
        }

        [HttpPost("adicionar")]
        public ActionResult Post(ServicoViewModel servico)
        {
            var result = _servicoRepositorie.Adicionar(new Servico{Preco = servico.Preco, Descricao = servico.Descricao});

            if (result != null)
                return Ok(result);

            return BadRequest();
        }
    }


}
