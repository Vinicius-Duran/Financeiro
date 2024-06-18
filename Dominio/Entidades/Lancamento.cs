using System;

namespace Dominio.Entidades
{
    public class Lancamento
    {
        public int Id { get; private set; }
        public string Descricao { get; private set; }
        public LancamentoTipo Tipo { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataVencimento { get; private set; }
        public int ReceitaId { get; private set; }
        public int CentroCustoId { get; private set; }
        public int ContaBancariaId { get; private set; }

        public virtual Receita Receita { get; private set; }
        public virtual CentroCusto CentroCusto { get; private set; }
        public virtual ContaBancaria ContaBancaria { get; private set; }

        public Lancamento(string descricao, LancamentoTipo tipo, decimal valor, DateTime dataVencimento, int receitaId, int centroCustoId, int contaBancariaId)
        {
            Descricao = descricao;
            Tipo = tipo;
            Valor = valor;
            DataVencimento = dataVencimento;
            ReceitaId = receitaId;
            CentroCustoId = centroCustoId;
            ContaBancariaId = contaBancariaId;
        }

        public void Atualizar(string descricao, LancamentoTipo tipo, decimal valor, DateTime dataVencimento, int receitaId, int centroCustoId, int contaBancariaId)
        {
            Descricao = descricao;
            Tipo = tipo;
            Valor = valor;
            DataVencimento = dataVencimento;
            ReceitaId = receitaId;
            CentroCustoId = centroCustoId;
            ContaBancariaId = contaBancariaId;
        }
    }

    public enum LancamentoTipo
    {
        Pagamento,
        Recebimento
    }
}
