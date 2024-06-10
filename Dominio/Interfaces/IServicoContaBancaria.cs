using Dominio.Argumentos;

namespace Dominio.Interfaces
{
    public interface IServicoContaBancaria
    {
        DTOContaBancaria Adicionar(DTOContaBancaria dtoContaBancaria);
        DTOContaBancaria Editar(DTOContaBancaria dtoContaBancaria);
        IEnumerable<DTOContaBancaria> Listar();
        DTOContaBancaria ObterPorId(int id);
        void Remover(int id);
    }
}
