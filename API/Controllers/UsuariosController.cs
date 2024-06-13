using Dominio.Argumentos;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IServicoUsuario _servicoUsuario;

        public UsuarioController(IServicoUsuario servicoUsuario)
        {
            _servicoUsuario = servicoUsuario;
        }

        [HttpPost]
        public IActionResult Adicionar([FromBody] DTOUsuario dtoUsuario)
        {
            var resultado = _servicoUsuario.Adicionar(dtoUsuario);
            if (_servicoUsuario.IsInvalid())
            {
                return BadRequest(_servicoUsuario.Notifications);
            }

            return Ok(resultado);
        }

        [HttpPut]
        public IActionResult Editar([FromBody] DTOUsuario dtoUsuario)
        {
            var resultado = _servicoUsuario.Editar(dtoUsuario);
            if (_servicoUsuario.IsInvalid())
            {
                return BadRequest(_servicoUsuario.Notifications);
            }

            return Ok(resultado);
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var resultado = _servicoUsuario.Listar();
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var resultado = _servicoUsuario.ObterPorId(id);
            if (_servicoUsuario.IsInvalid())
            {
                return NotFound(_servicoUsuario.Notifications);
            }

            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            _servicoUsuario.Remover(id);
            if (_servicoUsuario.IsInvalid())
            {
                return NotFound(_servicoUsuario.Notifications);
            }

            return NoContent();
        }
    }
}
