using AutoMapper;
using Dominio.Argumentos;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LancamentoController : ControllerBase
    {
        private readonly IServicoLancamento _servicoLancamento;
        private readonly IMapper _mapper;

        public LancamentoController(IServicoLancamento servicoLancamento, IMapper mapper)
        {
            _servicoLancamento = servicoLancamento;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DTOLancamento>> Listar()
        {
            var lancamentos = _servicoLancamento.Listar();
            return Ok(lancamentos);
        }

        [HttpGet("{id}")]
        public ActionResult<DTOLancamento> ObterPorId(int id)
        {
            var lancamento = _servicoLancamento.ObterPorId(id);
            if (lancamento == null)
            {
                return NotFound();
            }

            return Ok(lancamento);
        }

        [HttpPost]
        public ActionResult<DTOLancamento> Adicionar([FromBody] DTOLancamento dtoLancamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lancamento = _servicoLancamento.Adicionar(dtoLancamento);
            if (lancamento == null)
            {
                return BadRequest("Erro ao adicionar o lançamento.");
            }

            return CreatedAtAction(nameof(ObterPorId), new { id = lancamento.Id }, lancamento);
        }

        [HttpPut("{id}")]
        public ActionResult<DTOLancamento> Editar(int id, [FromBody] DTOLancamento dtoLancamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dtoLancamento.Id)
            {
                return BadRequest("ID da URL e ID do corpo não coincidem.");
            }

            var lancamento = _servicoLancamento.Editar(dtoLancamento);
            if (lancamento == null)
            {
                return NotFound("Lançamento não encontrado.");
            }

            return Ok(lancamento);
        }

        [HttpDelete("{id}")]
        public ActionResult Remover(int id)
        {
            var lancamento = _servicoLancamento.ObterPorId(id);
            if (lancamento == null)
            {
                return NotFound("Lançamento não encontrado.");
            }

            _servicoLancamento.Remover(id);
            return NoContent();
        }
    }
}
