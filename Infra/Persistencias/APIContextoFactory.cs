using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Infra.Persistencias
{
    public class APIContextoFactory : IDesignTimeDbContextFactory<APIContexto>
    {
        public APIContexto CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<APIContexto>();

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())  // Define o diretório base para o arquivo de configuração
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "..", "API", "appsettings.json"))  // Adiciona o arquivo de configuração JSON
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);

            return new APIContexto(optionsBuilder.Options);
        }
    }
}
