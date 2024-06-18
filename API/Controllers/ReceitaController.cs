using AutoMapper;
using Dominio.Argumentos;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceitaController : ControllerBase
    {
        private readonly IServicoReceita _servicoReceita;
        private readonly IMapper _mapper;

        public ReceitaController(IServicoReceita servicoReceita, IMapper mapper)
        {
            _servicoReceita = servicoReceita;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DTOReceita>> Listar()
        {
            var receitas = _servicoReceita.Listar();
            return Ok(receitas);
        }

        [HttpGet("{id}")]
        public ActionResult<DTOReceita> ObterPorId(int id)
        {
            var receita = _servicoReceita.ObterPorId(id);
            if (receita == null)
            {
                return NotFound();
            }

            return Ok(receita);
        }

        [HttpPost]
        public ActionResult<DTOReceita> Adicionar([FromBody] DTOReceita dtoReceita)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var receita = _servicoReceita.Adicionar(dtoReceita);
            if (receita == null)
            {
                return BadRequest("Erro ao adicionar a receita.");
            }

            return CreatedAtAction(nameof(ObterPorId), new { id = receita.Id }, receita);
        }

        [HttpPut("{id}")]
        public ActionResult<DTOReceita> Editar(int id, [FromBody] DTOReceita dtoReceita)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dtoReceita.Id)
            {
                return BadRequest("ID da URL e ID do corpo não coincidem.");
            }

            var receita = _servicoReceita.Editar(dtoReceita);
            if (receita == null)
            {
                return NotFound("Receita não encontrada.");
            }

            return Ok(receita);
        }

        [HttpDelete("{id}")]
        public ActionResult Remover(int id)
        {
            var receita = _servicoReceita.ObterPorId(id);
            if (receita == null)
            {
                return NotFound("Receita não encontrada.");
            }

            _servicoReceita.Remover(id);
            return NoContent();
        }
    }
}
