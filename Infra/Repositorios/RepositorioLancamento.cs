using Dominio.Entidades;
using Dominio.Interfaces.Repositorio;
using Infra.Persistencias;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorios
{
    public class RepositorioLancamento : Repositorio<Lancamento>, IRepositorioLancamento
    {
        public RepositorioLancamento(APIContexto context) : base(context) { }

        public IQueryable<Lancamento> Listar()
        {
            return _context.Lancamentos
                .Include(l => l.CentroCusto)
                .Include(l => l.Receita)
                .Include(l => l.ContaBancaria)
                .AsQueryable();
        }
    }
}
