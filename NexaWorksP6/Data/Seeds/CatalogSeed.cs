using Microsoft.EntityFrameworkCore;
using NexaWorksP6.Entities;

namespace NexaWorksP6.Data.Seeds
{
    public static class CatalogSeed
    {
        public static void SeedCatalog(ModelBuilder b)
        {
            b.Entity<Status>().HasData(
                new Status { Id = 1, Name = "En cours" },
                new Status { Id = 2, Name = "Résolu" }
            );

            b.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Trader en Herbe" },
                new Product { Id = 2, Name = "Maître des Investissements" },
                new Product { Id = 3, Name = "Planificateur d’Entraînement" },
                new Product { Id = 4, Name = "Planificateur d’Anxiété Sociale" }
            );

            b.Entity<VersionEntity>().HasData(
                // P1
                new VersionEntity { Id = 1, ProductId = 1, Name = "1.0" },
                new VersionEntity { Id = 2, ProductId = 1, Name = "1.1" },
                new VersionEntity { Id = 3, ProductId = 1, Name = "1.2" },
                new VersionEntity { Id = 4, ProductId = 1, Name = "1.3" },
                // P2
                new VersionEntity { Id = 5, ProductId = 2, Name = "1.0" },
                new VersionEntity { Id = 6, ProductId = 2, Name = "2.0" },
                new VersionEntity { Id = 7, ProductId = 2, Name = "2.1" },
                // P3
                new VersionEntity { Id = 8, ProductId = 3, Name = "1.0" },
                new VersionEntity { Id = 9, ProductId = 3, Name = "1.1" },
                new VersionEntity { Id = 10, ProductId = 3, Name = "2.0" },
                // P4
                new VersionEntity { Id = 11, ProductId = 4, Name = "1.0" },
                new VersionEntity { Id = 12, ProductId = 4, Name = "2.1" }
            );

            b.Entity<OperatingSystemEntity>().HasData(
                new OperatingSystemEntity { Id = 1, Name = "Windows" },
                new OperatingSystemEntity { Id = 2, Name = "Linux" },
                new OperatingSystemEntity { Id = 3, Name = "MacOS" },
                new OperatingSystemEntity { Id = 4, Name = "Android" },
                new OperatingSystemEntity { Id = 5, Name = "iOS" },
                new OperatingSystemEntity { Id = 6, Name = "Windows Mobile" }
            );
        }
    }
}
