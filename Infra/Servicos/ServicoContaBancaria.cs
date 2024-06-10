using Dominio.Argumentos;
using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Interfaces.Repositorio;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Servicos
{
    public class ServicoContaBancaria : IServicoContaBancaria
    {
        private readonly IRepositorioContaBancaria _repositorioContaBancaria;

        public ServicoContaBancaria(IRepositorioContaBancaria repositorioContaBancaria)
        {
            _repositorioContaBancaria = repositorioContaBancaria;
        }

        public DTOContaBancaria Adicionar(DTOContaBancaria dtoContaBancaria)
        {
            var contaBancaria = new ContaBancaria
            {
                Nome = dtoContaBancaria.Nome,
                Agencia = dtoContaBancaria.Agencia,
                Conta = dtoContaBancaria.Conta
            };

            _repositorioContaBancaria.Adicionar(contaBancaria);
            dtoContaBancaria.Id = contaBancaria.Id;
            return dtoContaBancaria;
        }

        public DTOContaBancaria Editar(DTOContaBancaria dtoContaBancaria)
        {
            var contaBancaria = _repositorioContaBancaria.ObterPorId(dtoContaBancaria.Id);
            if (contaBancaria != null)
            {
                contaBancaria.Nome = dtoContaBancaria.Nome;
                contaBancaria.Agencia = dtoContaBancaria.Agencia;
                contaBancaria.Conta = dtoContaBancaria.Conta;

                _repositorioContaBancaria.Editar(contaBancaria);
            }

            return dtoContaBancaria;
        }

        public IEnumerable<DTOContaBancaria> Listar()
        {
            return _repositorioContaBancaria.Listar().Select(p => new DTOContaBancaria
            {
                Id = p.Id,
                Nome = p.Nome,
                Agencia = p.Agencia,
                Conta = p.Conta
            }).ToList();
        }

        public DTOContaBancaria ObterPorId(int id)
        {
            var contaBancaria = _repositorioContaBancaria.ObterPorId(id);
            if (contaBancaria == null)
            {
                throw new Exception("Conta Bancária não encontrada.");
            }

            return new DTOContaBancaria
            {
                Id = contaBancaria.Id,
                Nome = contaBancaria.Nome,
                Agencia = contaBancaria.Agencia,
                Conta = contaBancaria.Conta
            };
        }

        public void Remover(int id)
        {
            _repositorioContaBancaria.Remover(id);
        }
    }
}
