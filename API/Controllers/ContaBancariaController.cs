using Dominio.Argumentos;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaBancariaController : ControllerBase
    {
        private readonly IServicoContaBancaria _servicoContaBancaria;

        public ContaBancariaController(IServicoContaBancaria servicoContaBancaria)
        {
            _servicoContaBancaria = servicoContaBancaria;
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody] DTOContaBancaria dtoContaBancaria)
        {
            var resultado = _servicoContaBancaria.Adicionar(dtoContaBancaria);
            if (_servicoContaBancaria.IsInvalid())
            {
                return BadRequest(_servicoContaBancaria.Notifications);
            }

            return Ok(resultado);
        }

        [HttpPut]
        public IActionResult Editar([FromBody] DTOContaBancaria dtoContaBancaria)
        {
            var resultado = _servicoContaBancaria.Editar(dtoContaBancaria);
            if (_servicoContaBancaria.IsInvalid())
            {
                return BadRequest(_servicoContaBancaria.Notifications);
            }

            return Ok(resultado);
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var resultado = _servicoContaBancaria.Listar();
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var resultado = _servicoContaBancaria.ObterPorId(id);
            if (_servicoContaBancaria.IsInvalid())
            {
                return NotFound(_servicoContaBancaria.Notifications);
            }

            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            _servicoContaBancaria.Remover(id);
            if (_servicoContaBancaria.IsInvalid())
            {
                return NotFound(_servicoContaBancaria.Notifications);
            }

            return NoContent();
        }
    }
}
