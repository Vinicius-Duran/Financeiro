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
    public class ServicoContaBancaria : Notifiable , IServicoContaBancaria
    {
        private readonly IRepositorioContaBancaria _repositorioContaBancaria;
        private readonly IMapper _mapper;

        public ServicoContaBancaria(IRepositorioContaBancaria repositorioContaBancaria, IMapper mapper)
        {
            _repositorioContaBancaria = repositorioContaBancaria;
            _mapper = mapper;
        }

        public DTOContaBancaria Adicionar(DTOContaBancaria dtoContaBancaria)
        {
            var contaBancaria = new ContaBancaria(dtoContaBancaria.Nome, dtoContaBancaria.Agencia, dtoContaBancaria.Conta);
            _repositorioContaBancaria.Adicionar(contaBancaria);
            return _mapper.Map<DTOContaBancaria>(contaBancaria);
        }

        public DTOContaBancaria Editar(DTOContaBancaria dtoContaBancaria)
        {
            var contaBancaria = _repositorioContaBancaria.ObterPorId(dtoContaBancaria.Id);
            if (contaBancaria == null)
            {
                throw new KeyNotFoundException("Conta bancária não encontrada.");
            }

            contaBancaria.Atualizar(dtoContaBancaria.Nome, dtoContaBancaria.Agencia, dtoContaBancaria.Conta);
            _repositorioContaBancaria.Editar(contaBancaria);

            return dtoContaBancaria;
        }

        public IEnumerable<DTOContaBancaria> Listar()
        {
            var contasBancarias = _repositorioContaBancaria.Listar();
            return _mapper.Map<IEnumerable<DTOContaBancaria>>(contasBancarias);
        }

        public DTOContaBancaria ObterPorId(int id)
        {
            var contaBancaria = _repositorioContaBancaria.ObterPorId(id);
            if (contaBancaria == null)
            {
                throw new KeyNotFoundException("Conta bancária não encontrada.");
            }

            return _mapper.Map<DTOContaBancaria>(contaBancaria);
        }

        public void Remover(int id)
        {
            var contaBancaria = _repositorioContaBancaria.ObterPorId(id);
            if (contaBancaria == null)
            {
                throw new KeyNotFoundException("Conta bancária não encontrada.");
            }

            _repositorioContaBancaria.Remover(id);
        }
    }
}
