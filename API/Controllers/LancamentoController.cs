using Dominio.Argumentos;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LancamentoController : ControllerBase
    {
        private readonly IServicoLancamento _servicoLancamento;

        public LancamentoController(IServicoLancamento servicoLancamento)
        {
            _servicoLancamento = servicoLancamento;
        }

        [HttpPost]
        public IActionResult AdicionarLancamento([FromBody] DTOLancamento dtoLancamento)
        {
            try
            {
                var novoLancamento = _servicoLancamento.Adicionar(dtoLancamento);
                return CreatedAtAction(nameof(ObterPorId), new { id = novoLancamento.Id }, novoLancamento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditarLancamento(int id, [FromBody] DTOLancamento dtoLancamento)
        {
            if (id != dtoLancamento.Id)
                return BadRequest(new { error = "ID mismatch" });

            try
            {
                var lancamentoEditado = _servicoLancamento.Editar(dtoLancamento);
                return Ok(lancamentoEditado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult ListarLancamento()
        {
            var lancamentos = _servicoLancamento.Listar();
            return Ok(lancamentos);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            try
            {
                var lancamento = _servicoLancamento.ObterPorId(id);
                return Ok(lancamento);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult RemoverLancamento(int id)
        {
            try
            {
                _servicoLancamento.Remover(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
