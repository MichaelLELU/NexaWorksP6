using Microsoft.EntityFrameworkCore;
using NexaWorksP6.Entities;

namespace NexaWorksP6.Data.Seeds
{
    public static class PvoSeed
    {
        public static void SeedPvo(ModelBuilder b)
        {
            // Trader en Herbe P1
            b.Entity<ProductVersionOs>().HasData(
                new { ProductId = 1, VersionId = 1, OperatingSystemId = 1 },
                new { ProductId = 1, VersionId = 1, OperatingSystemId = 2 },
                new { ProductId = 1, VersionId = 1, OperatingSystemId = 3 },
                new { ProductId = 1, VersionId = 2, OperatingSystemId = 2 },
                new { ProductId = 1, VersionId = 2, OperatingSystemId = 3 },
                new { ProductId = 1, VersionId = 3, OperatingSystemId = 2 },
                new { ProductId = 1, VersionId = 3, OperatingSystemId = 3 },
                new { ProductId = 1, VersionId = 3, OperatingSystemId = 4 },
                new { ProductId = 1, VersionId = 3, OperatingSystemId = 5 },
                new { ProductId = 1, VersionId = 3, OperatingSystemId = 6 },
                new { ProductId = 1, VersionId = 4, OperatingSystemId = 2 },
                new { ProductId = 1, VersionId = 4, OperatingSystemId = 3 }
            );

            // Maître des Investissements P2
            b.Entity<ProductVersionOs>().HasData(
                new { ProductId = 2, VersionId = 5, OperatingSystemId = 3 },
                new { ProductId = 2, VersionId = 6, OperatingSystemId = 3 },
                new { ProductId = 2, VersionId = 6, OperatingSystemId = 4 },
                new { ProductId = 2, VersionId = 6, OperatingSystemId = 5 },
                new { ProductId = 2, VersionId = 7, OperatingSystemId = 3 },
                new { ProductId = 2, VersionId = 7, OperatingSystemId = 4 },
                new { ProductId = 2, VersionId = 7, OperatingSystemId = 5 }
            );

            // Planificateur d’Entraînement P3
            b.Entity<ProductVersionOs>().HasData(
                new { ProductId = 3, VersionId = 8, OperatingSystemId = 2 },
                new { ProductId = 3, VersionId = 8, OperatingSystemId = 3 },
                new { ProductId = 3, VersionId = 9, OperatingSystemId = 2 },
                new { ProductId = 3, VersionId = 9, OperatingSystemId = 3 },
                new { ProductId = 3, VersionId = 9, OperatingSystemId = 4 },
                new { ProductId = 3, VersionId = 9, OperatingSystemId = 5 },
                new { ProductId = 3, VersionId = 10, OperatingSystemId = 2 },
                new { ProductId = 3, VersionId = 10, OperatingSystemId = 5 },
                new { ProductId = 3, VersionId = 10, OperatingSystemId = 3 },
                new { ProductId = 3, VersionId = 10, OperatingSystemId = 1 }
            );

            // Planificateur d’Anxiété Sociale P4
            b.Entity<ProductVersionOs>().HasData(
                new { ProductId = 4, VersionId = 11, OperatingSystemId = 1 },
                new { ProductId = 4, VersionId = 11, OperatingSystemId = 3 },
                new { ProductId = 4, VersionId = 11, OperatingSystemId = 4 },
                new { ProductId = 4, VersionId = 11, OperatingSystemId = 5 },
                new { ProductId = 4, VersionId = 12, OperatingSystemId = 1 },
                new { ProductId = 4, VersionId = 12, OperatingSystemId = 3 },
                new { ProductId = 4, VersionId = 12, OperatingSystemId = 4 },
                new { ProductId = 4, VersionId = 12, OperatingSystemId = 5 }
            );
        }
    }
}
