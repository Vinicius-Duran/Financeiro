namespace Dominio.Entidades
{
    public class Usuario
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public string Cpf { get; private set; }
        public DateTime DataNascimento { get; private set; }

        public Usuario(string nome, string email, string senha, string cpf, DateTime dataNascimento)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Cpf = cpf;
            DataNascimento = dataNascimento;
        }

        public void AtualizarUsuario(string nome, string email, string senha, string cpf, DateTime dataNascimento)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            Cpf = cpf;
            DataNascimento = dataNascimento;
        }
    }

}
