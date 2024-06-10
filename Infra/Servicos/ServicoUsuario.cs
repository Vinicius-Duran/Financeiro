using AutoMapper;
using Dominio.Argumentos;
using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Interfaces.Repositorio;
using System.Collections.Generic;

namespace Infra.Servicos
{
    public class ServicoUsuario : IServicoUsuario
    {
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IMapper _mapper;

        public ServicoUsuario(IRepositorioUsuario repositorioUsuario, IMapper mapper)
        {
            _repositorioUsuario = repositorioUsuario;
            _mapper = mapper;
        }

        public DTOUsuario Adicionar(DTOUsuario dtoUsuario)
        {
            var usuario = _mapper.Map<Usuario>(dtoUsuario);
            _repositorioUsuario.Adicionar(usuario);
            return _mapper.Map<DTOUsuario>(usuario);
        }

        public DTOUsuario Editar(DTOUsuario dtoUsuario)
        {
            var usuario = _repositorioUsuario.ObterPorId(dtoUsuario.Id);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            _mapper.Map(dtoUsuario, usuario);
            _repositorioUsuario.Editar(usuario);

            return dtoUsuario;
        }

        public IEnumerable<DTOUsuario> Listar()
        {
            var usuarios = _repositorioUsuario.Listar();
            return _mapper.Map<IEnumerable<DTOUsuario>>(usuarios);
        }

        public DTOUsuario ObterPorId(int id)
        {
            var usuario = _repositorioUsuario.ObterPorId(id);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            return _mapper.Map<DTOUsuario>(usuario);
        }

        public void Remover(int id)
        {
            var usuario = _repositorioUsuario.ObterPorId(id);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            _repositorioUsuario.Remover(id);
        }
    }
}
