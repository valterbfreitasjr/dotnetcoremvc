using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OficinaSystem.API.ViewModel;
using OficinaSystem.Domain.Entity;
using OficinaSystem.Domain.Interfaces;

namespace OficinaSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepositorie _clienteRepositorie;

        public ClienteController(IClienteRepositorie clienteRepositorie)
        {
            _clienteRepositorie = clienteRepositorie;
        }

        [HttpPost("adicionar")]
        public ActionResult Post(ClienteViewModel cliente)
        {
            var result = _clienteRepositorie.Adicionar(new Cliente{Nome = cliente.Nome, Cpf = cliente.Cpf, Endereco = cliente.Endereco});

            if (result != null)
                return Ok(result);

            return BadRequest();
        }

        [HttpGet]
        public ActionResult Get()
        {
            var result = _clienteRepositorie.ObterTodos();

            if (result.Count > 0)
                return Ok(result);

            return NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var result = _clienteRepositorie.ObterCliente(id);

            if (result != null)
                return Ok(result);

            return NotFound();
        }

        [HttpPost("alterar")]
        public ActionResult Put(ClienteViewModel cliente)
        {
            var result = _clienteRepositorie.EditarCliente(new Cliente{Nome = cliente.Nome, Cpf = cliente.Cpf, Endereco = cliente.Endereco, Id = cliente.Id });

            if (result)
                return Ok();

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var result = _clienteRepositorie.Delete(id);

            if (result)
                return Ok();

            return BadRequest();
        }
    }
}
