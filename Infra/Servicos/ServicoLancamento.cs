using Dominio.Argumentos;
using Dominio.Entidades;
using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Dominio.Interfaces.Repositorio;

namespace Infra.Servicos
{
    public class ServicoLancamento : IServicoLancamento
    {
        private readonly IRepositorioLancamento _repositorioLancamento;

        public ServicoLancamento(IRepositorioLancamento repositorioLancamento)
        {
            _repositorioLancamento = repositorioLancamento;
        }

        public DTOLancamento Adicionar(DTOLancamento dtoLancamento)
        {
            var lancamento = new Lancamento
            {
                Descricao = dtoLancamento.Descricao,
                Tipo = (Dominio.Entidades.LancamentoTipo)dtoLancamento.Tipo,
                Valor = dtoLancamento.Valor,
                DataVencimento = dtoLancamento.DataVencimento,
                ReceitaId = dtoLancamento.ReceitaId,
                CentroCustoId = dtoLancamento.CentroCustoId,
                ContaBancariaId = dtoLancamento.ContaBancariaId
            };

            _repositorioLancamento.Adicionar(lancamento);
            dtoLancamento.Id = lancamento.Id;
            return dtoLancamento;
        }

        public DTOLancamento Editar(DTOLancamento dtoLancamento)
        {
            var lancamento = _repositorioLancamento.ObterPorId(dtoLancamento.Id);
            if (lancamento == null)
            {
                throw new InvalidOperationException("Lançamento não encontrado.");
            }

            lancamento.Descricao = dtoLancamento.Descricao;
            lancamento.Tipo = (Dominio.Entidades.LancamentoTipo)dtoLancamento.Tipo;
            lancamento.Valor = dtoLancamento.Valor;
            lancamento.DataVencimento = dtoLancamento.DataVencimento;
            lancamento.ReceitaId = dtoLancamento.ReceitaId;
            lancamento.CentroCustoId = dtoLancamento.CentroCustoId;
            lancamento.ContaBancariaId = dtoLancamento.ContaBancariaId;

            _repositorioLancamento.Editar(lancamento);

            return dtoLancamento;
        }

        public IEnumerable<DTOLancamento> Listar()
        {
#pragma warning disable CS8601 // Possível atribuição de referência nula.
            return _repositorioLancamento.Listar()
                .Include(l => l.CentroCusto)
                .Include(l => l.Receita)
                .Include(l => l.ContaBancaria)
                .Select(l => new DTOLancamento
                {
                    Id = l.Id,
                    Descricao = l.Descricao,
                    Tipo = (Dominio.Argumentos.LancamentoTipo)l.Tipo,
                    Valor = l.Valor,
                    DataVencimento = l.DataVencimento,
                    ReceitaId = l.ReceitaId,
                    CentroCustoId = l.CentroCustoId,
                    ContaBancariaId = l.ContaBancariaId,
                    CentroCusto = l.CentroCusto == null ? null : new DTOCentroCusto
                    {
                        Id = l.CentroCusto.Id,
                        Nome = l.CentroCusto.Nome,
                        Cpf = l.CentroCusto.Cpf,
                        Email = l.CentroCusto.Email,
                        Celular = l.CentroCusto.Celular
                    },
                    Receita = l.Receita == null ? null : new DTOReceita
                    {
                        Id = l.Receita.Id,
                        Nome = l.Receita.Nome,
                        Cpf = l.Receita.Cpf,
                        Email = l.Receita.Email,
                        Celular = l.Receita.Celular
                    },
                    ContaBancaria = l.ContaBancaria == null ? null : new DTOContaBancaria
                    {
                        Id = l.ContaBancaria.Id,
                        Nome = l.ContaBancaria.Nome,
                        Agencia = l.ContaBancaria.Agencia,
                        Conta = l.ContaBancaria.Conta
                    }
                })
                .ToList();
#pragma warning restore CS8601 // Possível atribuição de referência nula.
        }

        public DTOLancamento ObterPorId(int id)
        {
            var lancamento = _repositorioLancamento.ObterPorId(id);
            if (lancamento == null)
            {
                throw new InvalidOperationException("Lançamento não encontrado.");
            }

            return new DTOLancamento
            {
                Id = lancamento.Id,
                Descricao = lancamento.Descricao,
                Tipo = (Dominio.Argumentos.LancamentoTipo)lancamento.Tipo,
                Valor = lancamento.Valor,
                DataVencimento = lancamento.DataVencimento,
                ReceitaId = lancamento.ReceitaId,
                CentroCustoId = lancamento.CentroCustoId,
                ContaBancariaId = lancamento.ContaBancariaId
            };
        }

        public void Remover(int id)
        {
            var lancamento = _repositorioLancamento.ObterPorId(id);
            if (lancamento == null)
            {
                throw new InvalidOperationException("Lançamento não encontrado.");
            }

            _repositorioLancamento.Remover(id);
        }
    }
}
