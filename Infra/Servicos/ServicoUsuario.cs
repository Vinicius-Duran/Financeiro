using Dominio.Argumentos;
using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Interfaces.Repositorio;
using Infra.Utilidade;

namespace Infra.Servicos
{
    public class ServicoUsuario : IServicoUsuario
    {
        private readonly IRepositorioUsuario _repositorioUsuario;

        public ServicoUsuario(IRepositorioUsuario repositorioUsuario)
        {
            _repositorioUsuario = repositorioUsuario;
        }

        public DTOUsuario Adicionar(DTOUsuario dtoUsuario)
        {
            var erros = new List<string>();

            if (dtoUsuario.Nome.Length < 10)
            {
                erros.Add("Nome completo obrigatorio ");
            }

            if (dtoUsuario.Cpf.Length != 11 || !dtoUsuario.Cpf.All(char.IsDigit))
            {
                erros.Add("Cpf deve conter 11 numeros e apenas numeros");
            }

            var idade = DateTime.Today.Year - dtoUsuario.DataNascimento.Year;
            if (dtoUsuario.DataNascimento.Date > DateTime.Today.AddYears(-idade))
            {
                idade--;
            }

            if (idade < 18)
            {
                erros.Add("A pessoa deve ser maior de 18 anos.");
            }

            var cpfexistente = _repositorioUsuario.Listar().Any(u => u.Cpf == dtoUsuario.Cpf);
            if (cpfexistente)
            {
                erros.Add("Cpf já cadastrado");
            }

            var emailexistente = _repositorioUsuario.Listar().Any(u => u.Email == dtoUsuario.Email);
            if (emailexistente)
            {
                erros.Add("Email já cadastrado");
            }

            if (erros.Any())
            {
                throw new ServicoException(string.Join("; ", erros), 400);
            }

            var usuario = new Usuario
            {
                Nome = dtoUsuario.Nome,
                Email = dtoUsuario.Email,
                Senha = dtoUsuario.Senha,
                Cpf = dtoUsuario.Cpf,
                DataNascimento = dtoUsuario.DataNascimento,
            };

            _repositorioUsuario.Adicionar(usuario);
            dtoUsuario.Id = usuario.Id;
            return dtoUsuario;
        }

        public DTOUsuario Editar(DTOUsuario dtoUsuario)
        {
            var usuario = _repositorioUsuario.ObterPorId(dtoUsuario.Id);
            if (usuario != null)
            {
                usuario.Nome = dtoUsuario.Nome;
                usuario.Email = dtoUsuario.Email;
                usuario.Senha = dtoUsuario.Senha;
                usuario.Cpf = dtoUsuario.Cpf;
                usuario.DataNascimento = dtoUsuario.DataNascimento;

                _repositorioUsuario.Editar(usuario);
            }

            return dtoUsuario;
        }

        public IEnumerable<DTOUsuario> Listar()
        {
            return _repositorioUsuario.Listar().Select(p => new DTOUsuario
            {
                Id = p.Id,
                Nome = p.Nome,
                Email = p.Email,
                Senha = p.Senha,
                Cpf = p.Cpf,
                DataNascimento = p.DataNascimento,
            }).ToList();
        }

        public DTOUsuario ObterPorId(int id)
        {
            var usuario = _repositorioUsuario.ObterPorId(id);
            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            return new DTOUsuario
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = usuario.Senha,
                Cpf = usuario.Cpf,
                DataNascimento = usuario.DataNascimento,
            };
        }

        public void Remover(int id)
        {
            var usuario = _repositorioUsuario.ObterPorId(id);
            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            _repositorioUsuario.Remover(id);
        }
    }
}
