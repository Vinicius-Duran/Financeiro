using Dominio.Argumentos;
using prmToolkit.NotificationPattern;

namespace Dominio.Interfaces
{
    public interface IServicoContaBancaria : INotifiable
    {
        DTOContaBancaria Adicionar(DTOContaBancaria dtoContaBancaria);
        DTOContaBancaria Editar(DTOContaBancaria dtoContaBancaria);
        IEnumerable<DTOContaBancaria> Listar();
        DTOContaBancaria ObterPorId(int id);
        void Remover(int id);
    }
}
