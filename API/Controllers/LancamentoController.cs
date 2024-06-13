using Dominio.Argumentos;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using prmToolkit.NotificationPattern;

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
        public IActionResult Adicionar(DTOLancamento dtoLancamento)
        {
            var resultado = _servicoLancamento.Adicionar(dtoLancamento);
            if (_servicoLancamento.IsInvalid())
            {
                return BadRequest(new { errors = _servicoLancamento.Notifications });
            }
            return Ok(resultado);
        }

        [HttpPut]
        public IActionResult Editar(DTOLancamento dtoLancamento)
        {
            var resultado = _servicoLancamento.Editar(dtoLancamento);
            if (_servicoLancamento.IsInvalid())
            {
                return BadRequest(new { errors = _servicoLancamento.Notifications });
            }
            return Ok(resultado);
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var resultado = _servicoLancamento.Listar();
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var resultado = _servicoLancamento.ObterPorId(id);
            if (_servicoLancamento.IsInvalid())
            {
                return BadRequest(new { errors = _servicoLancamento.Notifications });
            }
            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            _servicoLancamento.Remover(id);
            if (_servicoLancamento.IsInvalid())
            {
                return BadRequest(new { errors = _servicoLancamento.Notifications });
            }
            return Ok();
        }
    }
}
