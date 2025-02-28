﻿using AutoMapper;
using Dominio.Argumentos;
using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Interfaces.Repositorio;
using prmToolkit.NotificationPattern;
using System.Collections.Generic;

namespace Infra.Servicos
{
    public class ServicoLancamento : Notifiable, IServicoLancamento
    {
        private readonly IRepositorioLancamento _repositorioLancamento;
        private readonly IMapper _mapper;

        public ServicoLancamento(IRepositorioLancamento repositorioLancamento, IMapper mapper)
        {
            _repositorioLancamento = repositorioLancamento;
            _mapper = mapper;
        }

        public DTOLancamento Adicionar(DTOLancamento dtoLancamento)
        {
            var lancamento = new Lancamento(
                dtoLancamento.Descricao,
                (Dominio.Entidades.LancamentoTipo)dtoLancamento.Tipo,
                dtoLancamento.Valor,
                dtoLancamento.DataVencimento,
                dtoLancamento.ReceitaId,
                dtoLancamento.CentroCustoId,
                dtoLancamento.ContaBancariaId);

            _repositorioLancamento.Adicionar(lancamento);
            return _mapper.Map<DTOLancamento>(lancamento);
        }

        public DTOLancamento Editar(DTOLancamento dtoLancamento)
        {
            var lancamento = _repositorioLancamento.ObterPorId(dtoLancamento.Id);
            if (lancamento == null)
            {
                AddNotification("Lancamento", "Lançamento não encontrado.");
                return null;
            }

            lancamento.Atualizar(
                dtoLancamento.Descricao,
                (Dominio.Entidades.LancamentoTipo)dtoLancamento.Tipo,
                dtoLancamento.Valor,
                dtoLancamento.DataVencimento,
                dtoLancamento.ReceitaId,
                dtoLancamento.CentroCustoId,
                dtoLancamento.ContaBancariaId);

            _repositorioLancamento.Editar(lancamento);

            return _mapper.Map<DTOLancamento>(lancamento);
        }

        public IEnumerable<DTOLancamento> Listar()
        {
            var lancamentos = _repositorioLancamento.Listar();
            return _mapper.Map<IEnumerable<DTOLancamento>>(lancamentos);
        }

        public DTOLancamento ObterPorId(int id)
        {
            var lancamento = _repositorioLancamento.ObterPorId(id);
            if (lancamento == null)
            {
                AddNotification("Lancamento", "Lançamento não encontrado.");
                return null;
            }

            return _mapper.Map<DTOLancamento>(lancamento);
        }

        public void Remover(int id)
        {
            var lancamento = _repositorioLancamento.ObterPorId(id);
            if (lancamento == null)
            {
                AddNotification("Lancamento", "Lançamento não encontrado.");
                return;
            }

            _repositorioLancamento.Remover(id);
        }
    }
}
