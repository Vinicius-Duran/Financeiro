namespace Infra.Utilidade
{
    public class ServicoException : Exception
    {
        public int StatusCode { get; }

        public ServicoException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
