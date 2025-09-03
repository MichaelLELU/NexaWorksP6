using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace NexaWorksP6.Data
{
    // Utilisé par "dotnet ef" pour créer le contexte à design-time
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<NexaWorksContext>
    {
        public NexaWorksContext CreateDbContext(string[] args)
        {
            // BasePath = dossier courant lors de l'exécution de la commande
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true) // true: au cas où tu lances depuis un autre dossier
                .AddEnvironmentVariables()
                .Build();

            var conn = config.GetConnectionString("NexaWorks")
                       ?? "Server=localhost\\SQLEXPRESS;Database=NexaWorks;Trusted_Connection=True;TrustServerCertificate=True";

            var builder = new DbContextOptionsBuilder<NexaWorksContext>()
                .UseSqlServer(conn);

            return new NexaWorksContext(builder.Options);
        }
    }
}
