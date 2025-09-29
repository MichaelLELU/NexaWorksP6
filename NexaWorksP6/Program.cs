using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NexaWorksP6.Data;


var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory) // dossier d’exécution (bin\...)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

var conn = config.GetConnectionString("NexaWorks")
           ?? "Server=localhost\\SQLEXPRESS;Database=NexaWorks;Trusted_Connection=True;TrustServerCertificate=True";

var options = new DbContextOptionsBuilder<NexaWorksContext>()
    .UseSqlServer(conn)
    .Options;


using (var db = new NexaWorksContext(options))
{
    Console.WriteLine("Applying migrations...");
    db.Database.Migrate();
    Console.WriteLine("Done. Database created/updated with seed data!");

}
