using Dominio.Argumentos;
using Dominio.Interfaces;
using Infra.Utilidade;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IServicoUsuario _servicoUsuario;

        public UsuariosController(IServicoUsuario servicoUsuario)
        {
            _servicoUsuario = servicoUsuario;
        }

        [HttpPost]
        public IActionResult AdicionarUsuario([FromBody] DTOUsuario dtoUsuario)
        {
            try
            {
                var novoUsuario = _servicoUsuario.Adicionar(dtoUsuario);
                return CreatedAtAction(nameof(ObterPorId), new { id = novoUsuario.Id }, novoUsuario);
            }
            catch (ServicoException ex)
            {
                return StatusCode(ex.StatusCode, new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditarUsuario(int id, [FromBody] DTOUsuario dtoUsuario)
        {
            if (id != dtoUsuario.Id)
                return BadRequest(new { error = "ID mismatch" });

            try
            {
                var usuarioEditado = _servicoUsuario.Editar(dtoUsuario);
                return Ok(usuarioEditado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult ListarUsuarios()
        {
            var usuarios = _servicoUsuario.Listar();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            try
            {
                var usuario = _servicoUsuario.ObterPorId(id);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult RemoverUsuario(int id)
        {
            try
            {
                _servicoUsuario.Remover(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
