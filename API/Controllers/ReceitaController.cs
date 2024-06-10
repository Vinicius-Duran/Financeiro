using Dominio.Argumentos;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceitaController : ControllerBase
    {
        private readonly IServicoReceita _servicoReceita;

        public ReceitaController(IServicoReceita servicoReceita)
        {
            _servicoReceita = servicoReceita;
        }

        [HttpPost]
        public IActionResult AdicionarReceita([FromBody] DTOReceita dtoReceita)
        {
            try
            {
                var novaReceita = _servicoReceita.Adicionar(dtoReceita);
                return CreatedAtAction(nameof(ObterPorId), new { id = novaReceita.Id }, novaReceita);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditarReceita(int id, [FromBody] DTOReceita dtoReceita)
        {
            if (id != dtoReceita.Id)
                return BadRequest(new { error = "ID mismatch" });

            try
            {
                var receitaEditada = _servicoReceita.Editar(dtoReceita);
                return Ok(receitaEditada);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult ListarReceita()
        {
            var receitas = _servicoReceita.Listar();
            return Ok(receitas);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            try
            {
                var receita = _servicoReceita.ObterPorId(id);
                return Ok(receita);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult RemoverReceita(int id)
        {
            try
            {
                _servicoReceita.Remover(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
