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

        public ServicoCentroCusto(IRepositorioCentroCusto repositorioCentroCusto)
        {
            _repositorioCentroCusto = repositorioCentroCusto;
        }

        public DTOCentroCusto Adicionar(DTOCentroCusto dtoCentroCusto)
        {
            var centroCusto = new CentroCusto
            {
                Nome = dtoCentroCusto.Nome,
                Cpf = dtoCentroCusto.Cpf,
                Email = dtoCentroCusto.Email,
                Celular = dtoCentroCusto.Celular
            };

            _repositorioCentroCusto.Adicionar(centroCusto);
            dtoCentroCusto.Id = centroCusto.Id;
            return dtoCentroCusto;
        }

        public DTOCentroCusto Editar(DTOCentroCusto dtoCentroCusto)
        {
            var centroCusto = _repositorioCentroCusto.ObterPorId(dtoCentroCusto.Id);
            if (centroCusto != null)
            {
                centroCusto.Nome = dtoCentroCusto.Nome;
                centroCusto.Cpf = dtoCentroCusto.Cpf;
                centroCusto.Email = dtoCentroCusto.Email;
                centroCusto.Celular = dtoCentroCusto.Celular;

                _repositorioCentroCusto.Editar(centroCusto);
            }

            return dtoCentroCusto;
        }

        public IEnumerable<DTOCentroCusto> Listar()
        {
            return _repositorioCentroCusto.Listar().Select(p => new DTOCentroCusto
            {
                Id = p.Id,
                Nome = p.Nome,
                Cpf = p.Cpf,
                Email = p.Email,
                Celular = p.Celular
            }).ToList();
        }

        public DTOCentroCusto ObterPorId(int id)
        {
            var centroCusto = _repositorioCentroCusto.ObterPorId(id);
            if (centroCusto == null)
            {
                throw new Exception("Centro de Custo não encontrado.");
            }

            return new DTOCentroCusto
            {
                Id = centroCusto.Id,
                Nome = centroCusto.Nome,
                Cpf = centroCusto.Cpf,
                Email = centroCusto.Email,
                Celular = centroCusto.Celular
            };
        }

        public void Remover(int id)
        {
            _repositorioCentroCusto.Remover(id);
        }
    }
}
