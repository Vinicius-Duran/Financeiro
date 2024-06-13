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
            var receita = _mapper.Map<Receita>(dtoReceita);

            _repositorioReceita.Adicionar(receita);
            return _mapper.Map<DTOReceita>(receita);
        }

        public DTOReceita Editar(DTOReceita dtoReceita)
        {
            var receita = _repositorioReceita.ObterPorId(dtoReceita.Id);
            if (receita == null)
            {
                AddNotification("Receita", "Receita não encontrada.");
                return null;
            }

            _mapper.Map(dtoReceita, receita);
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
                AddNotification("Receita", "Receita não encontrada.");
                return null;
            }

            return _mapper.Map<DTOReceita>(receita);
        }

        public void Remover(int id)
        {
            var receita = _repositorioReceita.ObterPorId(id);
            if (receita == null)
            {
                AddNotification("Receita", "Receita não encontrada.");
                return;
            }

            _repositorioReceita.Remover(id);
        }
    }
}
