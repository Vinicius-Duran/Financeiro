using Dominio.Argumentos;
using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Interfaces.Repositorio;

namespace Infra.Servicos
{
    public class ServicoReceita : IServicoReceita
    {
        private readonly IRepositorioReceita _repositorioReceita;

        public ServicoReceita(IRepositorioReceita repositorioReceita)
        {
            _repositorioReceita = repositorioReceita;
        }

        public DTOReceita Adicionar(DTOReceita dtoReceita)
        {
            var receita = new Receita
            {
                Nome = dtoReceita.Nome,
                Cpf = dtoReceita.Cpf,
                Email = dtoReceita.Email,
                Celular = dtoReceita.Celular
            };

            _repositorioReceita.Adicionar(receita);
            dtoReceita.Id = receita.Id;
            return dtoReceita;
        }

        public DTOReceita Editar(DTOReceita dtoReceita)
        {
            var receita = _repositorioReceita.ObterPorId(dtoReceita.Id);
            if (receita != null)
            {
                receita.Nome = dtoReceita.Nome;
                receita.Cpf = dtoReceita.Cpf;
                receita.Email = dtoReceita.Email;
                receita.Celular = dtoReceita.Celular;

                _repositorioReceita.Editar(receita);
            }

            return dtoReceita;
        }

        public IEnumerable<DTOReceita> Listar()
        {
            return _repositorioReceita.Listar().Select(r => new DTOReceita
            {
                Id = r.Id,
                Nome = r.Nome,
                Cpf = r.Cpf,
                Email = r.Email,
                Celular = r.Celular
            }).ToList();
        }

        public DTOReceita ObterPorId(int id)
        {
            var receita = _repositorioReceita.ObterPorId(id);
            if (receita == null)
            {
                throw new Exception("Receita não encontrada.");
            }

            return new DTOReceita
            {
                Id = receita.Id,
                Nome = receita.Nome,
                Cpf = receita.Cpf,
                Email = receita.Email,
                Celular = receita.Celular
            };
        }

        public void Remover(int id)
        {
            var receita = _repositorioReceita.ObterPorId(id);
            if (receita == null)
            {
                throw new Exception("Receita não encontrada.");
            }

            _repositorioReceita.Remover(id);
        }
    }
}
