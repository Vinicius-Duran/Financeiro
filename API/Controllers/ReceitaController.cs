using Dominio.Argumentos;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using prmToolkit.NotificationPattern;

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
        public IActionResult Adicionar(DTOReceita dtoReceita)
        {
            var resultado = _servicoReceita.Adicionar(dtoReceita);
            if (_servicoReceita.IsInvalid())
            {
                return BadRequest(new { errors = _servicoReceita.Notifications });
            }
            return Ok(resultado);
        }

        [HttpPut]
        public IActionResult Editar(DTOReceita dtoReceita)
        {
            var resultado = _servicoReceita.Editar(dtoReceita);
            if (_servicoReceita.IsInvalid())
            {
                return BadRequest(new { errors = _servicoReceita.Notifications });
            }
            return Ok(resultado);
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var resultado = _servicoReceita.Listar();
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var resultado = _servicoReceita.ObterPorId(id);
            if (_servicoReceita.IsInvalid())
            {
                return BadRequest(new { errors = _servicoReceita.Notifications });
            }
            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            _servicoReceita.Remover(id);
            if (_servicoReceita.IsInvalid())
            {
                return BadRequest(new { errors = _servicoReceita.Notifications });
            }
            return Ok();
        }
    }
}
