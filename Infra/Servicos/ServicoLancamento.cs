using AutoMapper;
using Dominio.Argumentos;
using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Interfaces.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Servicos
{
    public class ServicoLancamento : IServicoLancamento
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
            var lancamento = _mapper.Map<Lancamento>(dtoLancamento);
            _repositorioLancamento.Adicionar(lancamento);
            return _mapper.Map<DTOLancamento>(lancamento);
        }

        public DTOLancamento Editar(DTOLancamento dtoLancamento)
        {
            var lancamento = _repositorioLancamento.ObterPorId(dtoLancamento.Id);
            if (lancamento == null)
            {
                throw new InvalidOperationException("Lançamento não encontrado.");
            }

            _mapper.Map(dtoLancamento, lancamento);
            _repositorioLancamento.Editar(lancamento);

            return dtoLancamento;
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
                throw new InvalidOperationException("Lançamento não encontrado.");
            }

            return _mapper.Map<DTOLancamento>(lancamento);
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
