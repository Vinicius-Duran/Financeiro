using AutoMapper;
using Dominio.Argumentos;
using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Interfaces.Repositorio;
using prmToolkit.NotificationPattern;
using System.Collections.Generic;

namespace Infra.Servicos
{
    public class ServicoUsuario : Notifiable, IServicoUsuario
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
           
            if (dtoUsuario.Nome.Length < 10)
            {
                AddNotification("Nome", "Nome completo obrigatório.");
            }
            if (dtoUsuario.Cpf.Length != 11 || !dtoUsuario.Cpf.All(char.IsDigit))
            {
                AddNotification("Cpf", "Cpf deve conter 11 números e apenas números.");
            }
            if (_repositorioUsuario.Existe(u => u.Cpf == dtoUsuario.Cpf))
            {
                AddNotification("Cpf", "Cpf já cadastrado.");
            }
            if (_repositorioUsuario.Existe(u => u.Email == dtoUsuario.Email))
            {
                AddNotification("Email", "Email já cadastrado.");
            }
            if (dtoUsuario.DataNascimento > DateTime.Now.AddYears(-18))
            {
                AddNotification("DataNascimento", "O usuário deve ser maior de 18 anos.");
            }

            if (IsInvalid())
            {
                return null;
            }

            var usuario = new Usuario(dtoUsuario.Nome, dtoUsuario.Email, dtoUsuario.Senha, dtoUsuario.Cpf, dtoUsuario.DataNascimento);
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

            usuario.AtualizarUsuario(dtoUsuario.Nome, dtoUsuario.Email, dtoUsuario.Senha, dtoUsuario.Cpf, dtoUsuario.DataNascimento);
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
