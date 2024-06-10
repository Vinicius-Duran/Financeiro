namespace Dominio.Entidades
{
    public class CentroCusto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public int Celular { get; set; }

        public virtual ICollection<Lancamento> Lancamentos { get; set; }
    }
}
