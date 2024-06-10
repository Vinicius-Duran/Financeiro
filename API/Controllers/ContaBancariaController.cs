using Dominio.Argumentos;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContaBancariaController : ControllerBase
    {
        private readonly IServicoContaBancaria _servicoContaBancaria;

        public ContaBancariaController(IServicoContaBancaria servicoContaBancaria)
        {
            _servicoContaBancaria = servicoContaBancaria;
        }

        [HttpPost]
        public IActionResult AdicionarContaBancaria([FromBody] DTOContaBancaria dtoContaBancaria)
        {
            try
            {
                var novaContaBancaria = _servicoContaBancaria.Adicionar(dtoContaBancaria);
                return CreatedAtAction(nameof(ObterPorId), new { id = novaContaBancaria.Id }, novaContaBancaria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditarContaBancaria(int id, [FromBody] DTOContaBancaria dtoContaBancaria)
        {
            if (id != dtoContaBancaria.Id)
                return BadRequest(new { error = "ID mismatch" });

            try
            {
                var contaBancariaEditada = _servicoContaBancaria.Editar(dtoContaBancaria);
                return Ok(contaBancariaEditada);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult ListarContaBancaria()
        {
            var contasBancarias = _servicoContaBancaria.Listar();
            return Ok(contasBancarias);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            try
            {
                var contaBancaria = _servicoContaBancaria.ObterPorId(id);
                return Ok(contaBancaria);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult RemoverContaBancaria(int id)
        {
            try
            {
                _servicoContaBancaria.Remover(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
