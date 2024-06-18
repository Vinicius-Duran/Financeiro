using AutoMapper;
using Dominio.Argumentos;
using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Interfaces.Repositorio;
using prmToolkit.NotificationPattern;
using System.Collections.Generic;

namespace Infra.Servicos
{
    public class ServicoReceita : Notifiable, IServicoReceita
    {
        private readonly IRepositorioReceita _repositorioReceita;
        private readonly IMapper _mapper;

        public ServicoReceita(IRepositorioReceita repositorioReceita, IMapper mapper)
        {
            _repositorioReceita = repositorioReceita;
            _mapper = mapper;
        }

        public DTOReceita Adicionar(DTOReceita dtoReceita)
        {
            

            if (IsInvalid())
            {
                return null;
            }

            var receita = new Receita(dtoReceita.Nome, dtoReceita.Cpf, dtoReceita.Email, dtoReceita.Celular);
            _repositorioReceita.Adicionar(receita);
            return _mapper.Map<DTOReceita>(receita);
        }

        public DTOReceita Editar(DTOReceita dtoReceita)
        {
            var receita = _repositorioReceita.ObterPorId(dtoReceita.Id);
            if (receita == null)
            {
                throw new KeyNotFoundException("Receita não encontrada.");
            }

            receita.AtualizarReceita(dtoReceita.Nome, dtoReceita.Cpf, dtoReceita.Email, dtoReceita.Celular);
            _repositorioReceita.Editar(receita);

            return dtoReceita;
        }

        public IEnumerable<DTOReceita> Listar()
        {
            var receitas = _repositorioReceita.Listar();
            return _mapper.Map<IEnumerable<DTOReceita>>(receitas);
        }

        public DTOReceita ObterPorId(int id)
        {
            var receita = _repositorioReceita.ObterPorId(id);
            if (receita == null)
            {
                throw new KeyNotFoundException("Receita não encontrada.");
            }

            return _mapper.Map<DTOReceita>(receita);
        }

        public void Remover(int id)
        {
            var receita = _repositorioReceita.ObterPorId(id);
            if (receita == null)
            {
                throw new KeyNotFoundException("Receita não encontrada.");
            }

            _repositorioReceita.Remover(id);
        }
    }
}
