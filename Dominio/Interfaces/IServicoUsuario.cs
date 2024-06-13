using Dominio.Argumentos;
using prmToolkit.NotificationPattern;

namespace Dominio.Interfaces
{
    public interface IServicoUsuario : INotifiable
    {
        DTOUsuario Adicionar(DTOUsuario dtoUsuario);
        DTOUsuario Editar(DTOUsuario dtoUsuario);
        IEnumerable<DTOUsuario> Listar();
        DTOUsuario ObterPorId(int id);
        void Remover(int id);
    }
}
