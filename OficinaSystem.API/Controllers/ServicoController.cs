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

        [HttpGet]
        public ActionResult Get()
        {
            var result = _servicoRepositorie.ObterTodos();

            if (result.Count > 0)
                return Ok(result);

            return NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var result = _servicoRepositorie.ObterServico(id);

            if (result != null)
                return Ok(result);

            return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, ServicoViewModel servico)
        {
            var result = _servicoRepositorie.EditarServico(new Servico { Preco = servico.Preco, Descricao = servico.Descricao, Id = id});

            //Se 1(true) - 0(false)
            if (result)
                return Ok();

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var result = _servicoRepositorie.Delete(id);

            if (result)
                return Ok();

            return BadRequest();
        }
    }


}
