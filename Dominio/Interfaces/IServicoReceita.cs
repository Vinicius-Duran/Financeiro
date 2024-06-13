using Dominio.Argumentos;
using prmToolkit.NotificationPattern;

namespace Dominio.Interfaces
{
    public interface IServicoReceita : INotifiable
    {
        DTOReceita Adicionar(DTOReceita dtoReceita);
        DTOReceita Editar(DTOReceita dtoReceita);
        IEnumerable<DTOReceita> Listar();
        DTOReceita ObterPorId(int id);
        void Remover(int id);
    }
}
