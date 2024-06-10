namespace Dominio.Entidades
{
    public class ContaBancaria
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Agencia { get; set; }
        public int Conta {  get; set; }

        public virtual ICollection<Lancamento> Lancamentos { get; set; }

    }
}
