using Dominio.Entidades;
using Dominio.Interfaces.Repositorio;
using Infra.Persistencias;

namespace Infra.Repositorios
{
    public class RepositorioReceita : Repositorio<Receita>, IRepositorioReceita
    {
        public RepositorioReceita(APIContexto context) : base(context) { }
    }
}
