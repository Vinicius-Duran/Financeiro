using Dominio.Entidades;
using Dominio.Interfaces.Repositorio;
using Infra.Persistencias;
using System.Linq.Expressions;

namespace Infra.Repositorios
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        protected readonly APIContexto _context;

        public Repositorio(APIContexto context)
        {
            _context = context; 
        }

        public T Adicionar(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public T Editar(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Existe(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Any(predicate);
        }

        public IEnumerable<T> Listar()
        {
            return _context.Set<T>().ToList();
        }

        public T ObterPorId(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Remover(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}
