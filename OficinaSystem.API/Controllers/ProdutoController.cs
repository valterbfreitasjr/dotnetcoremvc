using Microsoft.AspNetCore.Mvc;
using OficinaSystem.API.Queries;
using OficinaSystem.Domain.Entity;
using OficinaSystem.Domain.Interfaces;

namespace OficinaSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepositorie _produtoRepositorie;

        public ProdutoController(IProdutoRepositorie produtoRepositorie)
        {
            _produtoRepositorie = produtoRepositorie;
        }

        [HttpPost("adicionar")]
        public ActionResult Post(ProdutoViewModel produtoQuerie)
        {
            var result = _produtoRepositorie.Adiconar(new Produto{Descricao = produtoQuerie.Descricao, Preco = produtoQuerie.Preco});

            if(result != null)
              return Ok(result);

            return BadRequest();
        }

        [HttpGet]
        public ActionResult Get()
        {
            var result = _produtoRepositorie.ObterTodos();

            if(result.Count > 0) 
                return Ok(result);

            return NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var result = _produtoRepositorie.ObterProduto(id);

            if (result != null)
                return Ok(result);

            return NotFound();
        }

        [HttpPost("alterar")]
        public ActionResult Put(ProdutoViewModel produto)
        {
            var result = _produtoRepositorie.EditarProduto(new Produto{Descricao = produto.Descricao, Preco = produto.Preco, Id = produto.Id });

            //Se 1(true) - 0(false)
            if (result)
                return Ok();

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var result = _produtoRepositorie.Delete(id);

            if (result)
                return Ok();

            return BadRequest();
        }
    }
}
