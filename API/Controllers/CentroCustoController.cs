using AutoMapper;
using Dominio.Argumentos;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CentroCustoController : ControllerBase
    {
        private readonly IServicoCentroCusto _servicoCentroCusto;
        private readonly IMapper _mapper;

        public CentroCustoController(IServicoCentroCusto servicoCentroCusto, IMapper mapper)
        {
            _servicoCentroCusto = servicoCentroCusto;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DTOCentroCusto>> Listar()
        {
            var centroCustos = _servicoCentroCusto.Listar();
            return Ok(centroCustos);
        }

        [HttpGet("{id}")]
        public ActionResult<DTOCentroCusto> ObterPorId(int id)
        {
            var centroCusto = _servicoCentroCusto.ObterPorId(id);
            if (centroCusto == null)
            {
                return NotFound();
            }

            return Ok(centroCusto);
        }

        [HttpPost]
        public ActionResult<DTOCentroCusto> Adicionar([FromBody] DTOCentroCusto dtoCentroCusto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var centroCusto = _servicoCentroCusto.Adicionar(dtoCentroCusto);
            if (centroCusto == null)
            {
                return BadRequest("Erro ao adicionar o centro de custo.");
            }

            return CreatedAtAction(nameof(ObterPorId), new { id = centroCusto.Id }, centroCusto);
        }

        [HttpPut("{id}")]
        public ActionResult<DTOCentroCusto> Editar(int id, [FromBody] DTOCentroCusto dtoCentroCusto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dtoCentroCusto.Id)
            {
                return BadRequest("ID da URL e ID do corpo não coincidem.");
            }

            var centroCusto = _servicoCentroCusto.Editar(dtoCentroCusto);
            if (centroCusto == null)
            {
                return NotFound("Centro de custo não encontrado.");
            }

            return Ok(centroCusto);
        }

        [HttpDelete("{id}")]
        public ActionResult Remover(int id)
        {
            var centroCusto = _servicoCentroCusto.ObterPorId(id);
            if (centroCusto == null)
            {
                return NotFound("Centro de custo não encontrado.");
            }

            _servicoCentroCusto.Remover(id);
            return NoContent();
        }
    }
}
