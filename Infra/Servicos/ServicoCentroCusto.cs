using AutoMapper;
using Dominio.Argumentos;
using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Interfaces.Repositorio;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Servicos
{
    public class ServicoCentroCusto : IServicoCentroCusto
    {
        private readonly IRepositorioCentroCusto _repositorioCentroCusto;
        private readonly IMapper _mapper;

        public ServicoCentroCusto(IRepositorioCentroCusto repositorioCentroCusto, IMapper mapper)
        {
            _repositorioCentroCusto = repositorioCentroCusto;
            _mapper = mapper;
        }

        public DTOCentroCusto Adicionar(DTOCentroCusto dtoCentroCusto)
        {
            var centroCusto = _mapper.Map<CentroCusto>(dtoCentroCusto);
            _repositorioCentroCusto.Adicionar(centroCusto);
            return _mapper.Map<DTOCentroCusto>(centroCusto);
        }

        public DTOCentroCusto Editar(DTOCentroCusto dtoCentroCusto)
        {
            var centroCusto = _repositorioCentroCusto.ObterPorId(dtoCentroCusto.Id);
            if (centroCusto == null)
            {
                throw new KeyNotFoundException("Centro de Custo não encontrado.");
            }

            _mapper.Map(dtoCentroCusto, centroCusto);
            _repositorioCentroCusto.Editar(centroCusto);

            return dtoCentroCusto;
        }

        public IEnumerable<DTOCentroCusto> Listar()
        {
            var centroCustos = _repositorioCentroCusto.Listar();
            return _mapper.Map<IEnumerable<DTOCentroCusto>>(centroCustos);
        }

        public DTOCentroCusto ObterPorId(int id)
        {
            var centroCusto = _repositorioCentroCusto.ObterPorId(id);
            if (centroCusto == null)
            {
                throw new KeyNotFoundException("Centro de Custo não encontrado.");
            }

            return _mapper.Map<DTOCentroCusto>(centroCusto);
        }

        public void Remover(int id)
        {
            var centroCusto = _repositorioCentroCusto.ObterPorId(id);
            if (centroCusto == null)
            {
                throw new KeyNotFoundException("Centro de Custo não encontrado.");
            }

            _repositorioCentroCusto.Remover(id);
        }
    }
}
