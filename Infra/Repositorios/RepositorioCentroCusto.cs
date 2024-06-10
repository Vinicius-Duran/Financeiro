using Dominio.Entidades;
using Dominio.Interfaces.Repositorio;
using Infra.Persistencias;

namespace Infra.Repositorios
{
    public class RepositorioCentroCusto : Repositorio<CentroCusto>, IRepositorioCentroCusto
    {
        public RepositorioCentroCusto(APIContexto context) : base(context) { }
    }
}
