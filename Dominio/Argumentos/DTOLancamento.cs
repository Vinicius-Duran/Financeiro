using Dominio.Entidades;

namespace Dominio.Argumentos
{
    public class DTOLancamento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public LancamentoTipo Tipo { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }

        public int ReceitaId { get; set; }
        public int CentroCustoId { get; set; }
        public int ContaBancariaId { get; set; }
        public virtual DTOReceita Receita { get; set; }
        public virtual DTOCentroCusto CentroCusto { get; set; }
        public virtual DTOContaBancaria ContaBancaria { get; set; }
    }

    public enum LancamentoTipo
    {
        Pagamento,
        Recebimento
    }
}
