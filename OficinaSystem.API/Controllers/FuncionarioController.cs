using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OficinaSystem.API.ViewModel;
using OficinaSystem.Domain.Entity;
using OficinaSystem.Domain.Interfaces;

namespace OficinaSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly IFuncionarioRepositorie _funcionarioRepositorie;

        public FuncionarioController(IFuncionarioRepositorie funcionarioRepositorie)
        {
            _funcionarioRepositorie = funcionarioRepositorie;
        }

        [HttpPost("adicionar")]
        public ActionResult Post(FuncionarioViewModel funcionario)
        {
            var result = _funcionarioRepositorie.Adicionar(new Funcionario{Nome = funcionario.Nome, Cpf = funcionario.Cpf, Endereco = funcionario.Endereco});

            if (result != null)
                return Ok(result);

            return BadRequest();
        }

        [HttpGet("obtertodos")]
        public ActionResult Get()
        {
            var result = _funcionarioRepositorie.ObterTodos();

            if (result.Count > 0)
                return Ok(result);

            return NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var result = _funcionarioRepositorie.ObterFuncionario(id);

            if (result != null)
                return Ok(result);

            return NotFound();
        }

        [HttpPost("alterar")]
        public ActionResult Put(FuncionarioViewModel funcionario)
        {
            var result = _funcionarioRepositorie.EditarFuncionario(new Funcionario{Nome = funcionario.Nome, Cpf = funcionario.Cpf, Endereco = funcionario.Endereco, Id = funcionario .Id });

            //Se 1(true) - 0(false)
            if (result)
                return Ok(1);

            return BadRequest(0);
        }

        [HttpPost("deletar")]
        public ActionResult Delete([FromBody] int id)
        {
            var result = _funcionarioRepositorie.Delete(id);

            if (result)
                return Ok(1);

            return BadRequest(0);
        }
    }
}
