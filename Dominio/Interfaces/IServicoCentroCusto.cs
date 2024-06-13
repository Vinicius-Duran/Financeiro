using Dominio.Argumentos;
using prmToolkit.NotificationPattern;

namespace Dominio.Interfaces
{
    public interface IServicoCentroCusto : INotifiable
    {
        DTOCentroCusto Adicionar(DTOCentroCusto dtoCentroCusto);
        DTOCentroCusto Editar(DTOCentroCusto dtoCentroCusto);
        IEnumerable<DTOCentroCusto> Listar();
        DTOCentroCusto ObterPorId(int id);
        void Remover(int id);
    }
}
