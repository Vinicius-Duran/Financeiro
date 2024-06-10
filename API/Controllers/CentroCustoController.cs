using Dominio.Argumentos;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CentroCustoController : ControllerBase
    {
        private readonly IServicoCentroCusto _servicoCentroCusto;

        public CentroCustoController(IServicoCentroCusto servicoCentroCusto)
        {
            _servicoCentroCusto = servicoCentroCusto;
        }

        [HttpPost]
        public IActionResult AdicionarCentroCusto([FromBody] DTOCentroCusto dtoCentroCusto)
        {
            try
            {
                var novoCentroCusto = _servicoCentroCusto.Adicionar(dtoCentroCusto);
                return CreatedAtAction(nameof(ObterPorId), new { id = novoCentroCusto.Id }, novoCentroCusto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditarCentroCusto(int id, [FromBody] DTOCentroCusto dtoCentroCusto)
        {
            if (id != dtoCentroCusto.Id)
                return BadRequest(new { error = "ID mismatch" });

            try
            {
                var centroCustoEditado = _servicoCentroCusto.Editar(dtoCentroCusto);
                return Ok(centroCustoEditado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult ListarCentroCusto()
        {
            var centroCustos = _servicoCentroCusto.Listar();
            return Ok(centroCustos);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            try
            {
                var centroCusto = _servicoCentroCusto.ObterPorId(id);
                return Ok(centroCusto);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult RemoverCentroCusto(int id)
        {
            try
            {
                _servicoCentroCusto.Remover(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
