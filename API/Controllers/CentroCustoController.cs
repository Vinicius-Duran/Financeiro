using Dominio.Argumentos;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CentroCustoController : ControllerBase
    {
        private readonly IServicoCentroCusto _servicoCentroCusto;

        public CentroCustoController(IServicoCentroCusto servicoCentroCusto)
        {
            _servicoCentroCusto = servicoCentroCusto;
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody] DTOCentroCusto dtoCentroCusto)
        {
            var resultado = _servicoCentroCusto.Adicionar(dtoCentroCusto);
            if (_servicoCentroCusto.IsInvalid())
            {
                return BadRequest(_servicoCentroCusto.Notifications);
            }

            return Ok(resultado);
        }

        [HttpPut]
        public IActionResult Editar([FromBody] DTOCentroCusto dtoCentroCusto)
        {
            var resultado = _servicoCentroCusto.Editar(dtoCentroCusto);
            if (_servicoCentroCusto.IsInvalid())
            {
                return BadRequest(_servicoCentroCusto.Notifications);
            }

            return Ok(resultado);
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var resultado = _servicoCentroCusto.Listar();
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var resultado = _servicoCentroCusto.ObterPorId(id);
            if (_servicoCentroCusto.IsInvalid())
            {
                return NotFound(_servicoCentroCusto.Notifications);
            }

            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            _servicoCentroCusto.Remover(id);
            if (_servicoCentroCusto.IsInvalid())
            {
                return NotFound(_servicoCentroCusto.Notifications);
            }

            return NoContent();
        }
    }
}
