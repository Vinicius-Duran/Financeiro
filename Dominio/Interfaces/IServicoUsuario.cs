using Dominio.Argumentos;

namespace Dominio.Interfaces
{
    public interface IServicoUsuario
    {
        DTOUsuario Adicionar(DTOUsuario dtoUsuario);
        DTOUsuario Editar(DTOUsuario dtoUsuario);
        IEnumerable<DTOUsuario> Listar();
        DTOUsuario ObterPorId(int id);
        void Remover(int id);
    }
}
