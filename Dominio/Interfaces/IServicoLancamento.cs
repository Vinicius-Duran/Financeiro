using Dominio.Argumentos;

namespace Dominio.Interfaces
{
    public interface IServicoLancamento
    {
        DTOLancamento Adicionar(DTOLancamento dtoLancamento);
        DTOLancamento Editar(DTOLancamento dtoLancamento);
        IEnumerable<DTOLancamento> Listar();
        DTOLancamento ObterPorId(int id);
        void Remover(int id);
    }
}
