using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OficinaSystem.API.ViewModel;
using OficinaSystem.Domain.Entity;
using OficinaSystem.Domain.Interfaces;
using OficinaSystem.Domain.Services.Interfaces;

namespace OficinaSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;
        private readonly IPedidoRepositorie _pedidoRepositorie;

        public PedidoController(IPedidoService pedidoService, IPedidoRepositorie pedidoRepositorie)
        {
            _pedidoService = pedidoService;
            _pedidoRepositorie = pedidoRepositorie;
        }

        [HttpPost("adicionar")]
        public ActionResult Post(Pedido pedidoQuerie)
        {
            var result = _pedidoService.AdicionarPedidos(pedidoQuerie);

            if (result != 1)
                return BadRequest();

            return Ok();
        }

        [HttpGet("obtertodos")]
        public ActionResult Get()
        {
            var result = _pedidoRepositorie.ObterTodos();

            if (result.Count > 0)
                return Ok(result);

            return NotFound();
        }
    }
}
