using AutoMapper;
using Dominio.Argumentos;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaBancariaController : ControllerBase
    {
        private readonly IServicoContaBancaria _servicoContaBancaria;
        private readonly IMapper _mapper;

        public ContaBancariaController(IServicoContaBancaria servicoContaBancaria, IMapper mapper)
        {
            _servicoContaBancaria = servicoContaBancaria;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DTOContaBancaria>> Listar()
        {
            var contasBancarias = _servicoContaBancaria.Listar();
            return Ok(contasBancarias);
        }

        [HttpGet("{id}")]
        public ActionResult<DTOContaBancaria> ObterPorId(int id)
        {
            var contaBancaria = _servicoContaBancaria.ObterPorId(id);
            if (contaBancaria == null)
            {
                return NotFound();
            }

            return Ok(contaBancaria);
        }

        [HttpPost]
        public ActionResult<DTOContaBancaria> Adicionar([FromBody] DTOContaBancaria dtoContaBancaria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contaBancaria = _servicoContaBancaria.Adicionar(dtoContaBancaria);
            if (contaBancaria == null)
            {
                return BadRequest("Erro ao adicionar a conta bancária.");
            }

            return CreatedAtAction(nameof(ObterPorId), new { id = contaBancaria.Id }, contaBancaria);
        }

        [HttpPut("{id}")]
        public ActionResult<DTOContaBancaria> Editar(int id, [FromBody] DTOContaBancaria dtoContaBancaria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dtoContaBancaria.Id)
            {
                return BadRequest("ID da URL e ID do corpo não coincidem.");
            }

            var contaBancaria = _servicoContaBancaria.Editar(dtoContaBancaria);
            if (contaBancaria == null)
            {
                return NotFound("Conta bancária não encontrada.");
            }

            return Ok(contaBancaria);
        }

        [HttpDelete("{id}")]
        public ActionResult Remover(int id)
        {
            var contaBancaria = _servicoContaBancaria.ObterPorId(id);
            if (contaBancaria == null)
            {
                return NotFound("Conta bancária não encontrada.");
            }

            _servicoContaBancaria.Remover(id);
            return NoContent();
        }
    }
}
