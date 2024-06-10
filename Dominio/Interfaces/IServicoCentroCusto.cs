using Dominio.Argumentos;

namespace Dominio.Interfaces
{
    public interface IServicoCentroCusto
    {
        DTOCentroCusto Adicionar(DTOCentroCusto dtoCentroCusto);
        DTOCentroCusto Editar(DTOCentroCusto dtoCentroCusto);
        IEnumerable<DTOCentroCusto> Listar();
        DTOCentroCusto ObterPorId(int id);
        void Remover(int id);
    }
}
