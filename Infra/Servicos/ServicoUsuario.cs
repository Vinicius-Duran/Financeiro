using AutoMapper;
using Dominio.Argumentos;
using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Interfaces.Repositorio;
using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System.Collections.Generic;
using System.Linq;

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
            var usuario = _mapper.Map<Usuario>(dtoUsuario);

            
            if (usuario.Nome.Length < 10)
            {
                AddNotification("Nome", "Nome completo obrigatorio.");
            }
            if (usuario.Cpf.Length != 11 || !usuario.Cpf.All(char.IsDigit))
            {
                AddNotification("Cpf", "Cpf deve conter 11 números e apenas números.");
            }
            if (_repositorioUsuario.Existe(u => u.Cpf == usuario.Cpf))
            {
                AddNotification("Cpf", "Cpf já cadastrado.");
            }
            if (_repositorioUsuario.Existe(u => u.Email == usuario.Email))
            {
                AddNotification("Email", "Email já cadastrado.");
            }
            if (usuario.DataNascimento > DateTime.Now.AddYears(-18))
            {
                AddNotification("DataNascimento", "O usuário deve ser maior de 18 anos.");
            }

            if (IsInvalid())
            {
                return null;
            }

            _repositorioUsuario.Adicionar(usuario);
            return _mapper.Map<DTOUsuario>(usuario);
        }

        public DTOUsuario Editar(DTOUsuario dtoUsuario)
        {
            var usuario = _repositorioUsuario.ObterPorId(dtoUsuario.Id);
            if (usuario == null)
            {
                AddNotification("Usuario", "Usuário não encontrado.");
                return null;
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
                AddNotification("Usuario", "Usuário não encontrado.");
                return null;
            }

            return _mapper.Map<DTOUsuario>(usuario);
        }

        public void Remover(int id)
        {
            var usuario = _repositorioUsuario.ObterPorId(id);
            if (usuario == null)
            {
                AddNotification("Usuario", "Usuário não encontrado.");
                return;
            }

            _repositorioUsuario.Remover(id);
        }
    }
}
