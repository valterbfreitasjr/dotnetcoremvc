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

        [HttpPost]
        public ActionResult Post(FuncionarioViewModel funcionario)
        {
            var result = _funcionarioRepositorie.Adicionar(new Funcionario{Nome = funcionario.Nome, Cpf = funcionario.Cpf, Endereco = funcionario.Endereco});

            if (result != null)
                return Ok(result);

            return BadRequest();
        }

        [HttpGet]
        [Route("obtertodos")]
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

        [HttpPut("{id}")]
        public ActionResult Put(int id, FuncionarioViewModel funcionario)
        {
            var result = _funcionarioRepositorie.EditarFuncionario(new Funcionario{Nome = funcionario.Nome, Cpf = funcionario.Cpf, Endereco = funcionario.Endereco, Id = id});

            //Se 1(true) - 0(false)
            if (result)
                return Ok();

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var result = _funcionarioRepositorie.Delete(id);

            if (result)
                return Ok();

            return BadRequest();
        }
    }
}
