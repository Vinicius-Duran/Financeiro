using AutoMapper;
using Dominio.Argumentos;
using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Interfaces.Repositorio;
using prmToolkit.NotificationPattern;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Servicos
{
    public class ServicoCentroCusto : Notifiable, IServicoCentroCusto
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

            if (_repositorioCentroCusto.Existe(c => c.Nome == centroCusto.Nome))
            {
                AddNotification("CentroCusto", "Centro de Custo já existe.");
                return null;
            }

            _repositorioCentroCusto.Adicionar(centroCusto);
            return _mapper.Map<DTOCentroCusto>(centroCusto);
        }

        public DTOCentroCusto Editar(DTOCentroCusto dtoCentroCusto)
        {
            var centroCusto = _repositorioCentroCusto.ObterPorId(dtoCentroCusto.Id);
            if (centroCusto == null)
            {
                AddNotification("CentroCusto", "Centro de Custo não encontrado.");
                return null;
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
                AddNotification("CentroCusto", "Centro de Custo não encontrado.");
                return null;
            }

            return _mapper.Map<DTOCentroCusto>(centroCusto);
        }

        public void Remover(int id)
        {
            var centroCusto = _repositorioCentroCusto.ObterPorId(id);
            if (centroCusto == null)
            {
                AddNotification("CentroCusto", "Centro de Custo não encontrado.");
                return;
            }

            _repositorioCentroCusto.Remover(id);
        }
    }
}
