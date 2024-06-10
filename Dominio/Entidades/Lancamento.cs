namespace Dominio.Entidades
{
    public class Lancamento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public LancamentoTipo Tipo {  get; set; }
        public decimal Valor {  get; set; }
        public DateTime DataVencimento { get; set; }

        public int ReceitaId { get; set; }
        public int CentroCustoId { get; set; }
        public int ContaBancariaId { get; set; }
        public virtual Receita Receita { get; set; }
        public virtual CentroCusto CentroCusto { get; set; }
        public virtual ContaBancaria ContaBancaria { get; set;}
    }

    public enum LancamentoTipo
    {
        Pagamento,
        Recebimento
    }
}
