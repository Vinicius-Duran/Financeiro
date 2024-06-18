using AutoMapper;
using Dominio.Argumentos;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IServicoUsuario _servicoUsuario;
        private readonly IMapper _mapper;

        public UsuarioController(IServicoUsuario servicoUsuario, IMapper mapper)
        {
            _servicoUsuario = servicoUsuario;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DTOUsuario>> Listar()
        {
            var usuarios = _servicoUsuario.Listar();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public ActionResult<DTOUsuario> ObterPorId(int id)
        {
            var usuario = _servicoUsuario.ObterPorId(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpPost]
        public ActionResult<DTOUsuario> Adicionar([FromBody] DTOUsuario dtoUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = _servicoUsuario.Adicionar(dtoUsuario);
            if (usuario == null)
            {
                return BadRequest("Erro ao adicionar o usuário.");
            }

            return CreatedAtAction(nameof(ObterPorId), new { id = usuario.Id }, usuario);
        }

        [HttpPut("{id}")]
        public ActionResult<DTOUsuario> Editar(int id, [FromBody] DTOUsuario dtoUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dtoUsuario.Id)
            {
                return BadRequest("ID da URL e ID do corpo não coincidem.");
            }

            var usuario = _servicoUsuario.Editar(dtoUsuario);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public ActionResult Remover(int id)
        {
            var usuario = _servicoUsuario.ObterPorId(id);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            _servicoUsuario.Remover(id);
            return NoContent();
        }
    }
}
