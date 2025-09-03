using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NexaWorksP6.Data;
using NexaWorksP6.Demo;

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

    // Démo LINQ
    await QueriesDemo.ShowTicketsCount(db);
    await QueriesDemo.ShowTicketsEnCours(db);
    await QueriesDemo.ShowDerniersResolus(db);
    await QueriesDemo.ShowTicketsParProduit(db);
    await QueriesDemo.SearchTickets(db, "Crash");
    await QueriesDemo.ShowTicketsPage(db, 1, 5);
    await QueriesDemo.ShowCompatibilites(db);
    QueriesDemo.ShowSqlExample(db);
}
