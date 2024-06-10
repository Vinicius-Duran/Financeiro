using Dominio.Entidades;
using Dominio.Interfaces.Repositorio;
using Infra.Persistencias;

namespace Infra.Repositorios
{
    public class RepositorioUsuario : Repositorio<Usuario>, IRepositorioUsuario
    {
        public RepositorioUsuario(APIContexto context) : base(context) { }
    }
}
