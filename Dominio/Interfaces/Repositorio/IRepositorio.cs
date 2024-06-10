using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.Repositorio
{
    public interface IRepositorio<T> where T : class
    {
        T Adicionar(T entity);
        T Editar(T entity);
        IEnumerable<T> Listar();
        T ObterPorId(int id);
        void Remover(int id);
    }
}
