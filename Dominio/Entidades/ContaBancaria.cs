namespace Dominio.Entidades
{
    public class ContaBancaria
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public int Agencia { get; private set; }
        public int Conta { get; private set; }

        public virtual ICollection<Lancamento> Lancamentos { get; private set; }

        public ContaBancaria(string nome, int agencia, int conta)
        {
            Nome = nome;
            Agencia = agencia;
            Conta = conta;
            Lancamentos = new List<Lancamento>();
        }

        public void Atualizar(string nome, int agencia, int conta)
        {
            Nome = nome;
            Agencia = agencia;
            Conta = conta;
        }
    }
}
