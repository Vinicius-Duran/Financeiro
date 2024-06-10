using Dominio.Entidades;
using Dominio.Interfaces.Repositorio;
using Infra.Persistencias;

namespace Infra.Repositorios
{
    public class RepositorioContaBancaria : Repositorio<ContaBancaria>, IRepositorioContaBancaria
    {
        public RepositorioContaBancaria(APIContexto context) : base(context) { }
    }
}
