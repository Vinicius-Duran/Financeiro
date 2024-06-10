using Dominio.Argumentos;

namespace Dominio.Interfaces
{
    public interface IServicoReceita
    {
        DTOReceita Adicionar(DTOReceita dtoReceita);
        DTOReceita Editar(DTOReceita dtoReceita);
        IEnumerable<DTOReceita> Listar();
        DTOReceita ObterPorId(int id);
        void Remover(int id);
    }
}
