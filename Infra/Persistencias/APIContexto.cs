using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Persistencias
{
    public class APIContexto : DbContext
    {
        public APIContexto(DbContextOptions<APIContexto> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<CentroCusto> CentroCustos { get; set; }
        public DbSet<ContaBancaria> ContaBancarias { get; set; }
        public DbSet<Lancamento> Lancamentos { get; set; }
        public DbSet<Receita> Receitas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lancamento>()
                .Property(l => l.DataVencimento)
                .HasColumnType("date");

            modelBuilder.Entity<Usuario>()
                .Property(u => u.DataNascimento)
                .HasColumnType("date");

            modelBuilder.Entity<Lancamento>()
                .HasOne(l => l.Receita)
                .WithMany(r => r.Lancamentos)
                .HasForeignKey(l => l.ReceitaId);

            modelBuilder.Entity<Lancamento>()
                .HasOne(l => l.CentroCusto)
                .WithMany(cc => cc.Lancamentos)
                .HasForeignKey(l => l.CentroCustoId);

            modelBuilder.Entity<Lancamento>()
                .HasOne(l => l.ContaBancaria)
                .WithMany(cb => cb.Lancamentos)
                .HasForeignKey(l => l.ContaBancariaId);
        }
    }
}
