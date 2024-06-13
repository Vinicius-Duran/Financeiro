using Dominio.Argumentos;
using prmToolkit.NotificationPattern;

namespace Dominio.Interfaces
{
    public interface IServicoLancamento : INotifiable
    {
        DTOLancamento Adicionar(DTOLancamento dtoLancamento);
        DTOLancamento Editar(DTOLancamento dtoLancamento);
        IEnumerable<DTOLancamento> Listar();
        DTOLancamento ObterPorId(int id);
        void Remover(int id);
    }
}
