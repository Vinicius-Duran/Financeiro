namespace Dominio.Entidades
{
    public class CentroCusto
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public int Celular { get; private set; }

        public virtual ICollection<Lancamento> Lancamentos { get; private set; }

        public CentroCusto(string nome, string cpf, string email, int celular)
        {
            Nome = nome;
            Cpf = cpf;
            Email = email;
            Celular = celular;
        }

        public void Atualizar(string nome, string cpf, string email, int celular)
        {
            Nome = nome;
            Cpf = cpf;
            Email = email;
            Celular = celular;
        }
    }

}
