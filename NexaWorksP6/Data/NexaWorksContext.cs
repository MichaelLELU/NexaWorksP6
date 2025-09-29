using Microsoft.EntityFrameworkCore;
using NexaWorksP6.Entities;
using NexaWorksP6.Data.Seeds;

namespace NexaWorksP6.Data
{
    public class NexaWorksContext : DbContext
    {
        public NexaWorksContext(DbContextOptions<NexaWorksContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<VersionEntity> Versions => Set<VersionEntity>();
        public DbSet<OperatingSystemEntity> OperatingSystems => Set<OperatingSystemEntity>();
        public DbSet<Status> Statuses => Set<Status>();
        public DbSet<ProductVersionOs> ProductVersionOs => Set<ProductVersionOs>();
        public DbSet<Ticket> Tickets => Set<Ticket>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            // config des clés & relations (inchangée)
            ConfigureModel(b);

            // seeds séparés
            CatalogSeed.SeedCatalog(b);
            PvoSeed.SeedPvo(b);
            TicketSeed.SeedTickets(b);
        }

        private void ConfigureModel(ModelBuilder b)
        {

            b.Entity<Product>().HasKey(x => x.Id);
            b.Entity<VersionEntity>().HasKey(x => x.Id);
            b.Entity<OperatingSystemEntity>().HasKey(x => x.Id);
            b.Entity<Status>().HasKey(x => x.Id);

  
            b.Entity<VersionEntity>()
                .HasOne(v => v.Product)
                .WithMany(p => p.Versions)
                .HasForeignKey(v => v.ProductId)
                .OnDelete(DeleteBehavior.Restrict);


            b.Entity<ProductVersionOs>()
                .HasKey(x => new { x.ProductId, x.VersionId, x.OperatingSystemId });


            b.Entity<ProductVersionOs>()
                .HasOne(pvo => pvo.Product)
                .WithMany(p => p.ProductVersionOs)
                .HasForeignKey(pvo => pvo.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            b.Entity<ProductVersionOs>()
                .HasOne(pvo => pvo.Version)
                .WithMany(v => v.ProductVersionOs)
                .HasForeignKey(pvo => pvo.VersionId)
                .OnDelete(DeleteBehavior.Restrict);

            b.Entity<ProductVersionOs>()
                .HasOne(pvo => pvo.OperatingSystem)
                .WithMany(os => os.ProductVersionOs)
                .HasForeignKey(pvo => pvo.OperatingSystemId)
                .OnDelete(DeleteBehavior.Restrict);
            b.Entity<Ticket>()
                .HasOne(t => t.Pvo)
                .WithMany(pvo => pvo.Tickets)
                .HasForeignKey(t => new { t.ProductId, t.VersionId, t.OperatingSystemId })
                .OnDelete(DeleteBehavior.Restrict);


            b.Entity<Ticket>()
                .HasOne(t => t.Status)
                .WithMany(s => s.Tickets)
                .HasForeignKey(t => t.StatusId)
                .OnDelete(DeleteBehavior.Restrict);


            b.Entity<VersionEntity>()
                .HasIndex(v => new { v.ProductId, v.Name })
                .IsUnique();

            b.Entity<ProductVersionOs>()
                .HasIndex(pvo => new { pvo.ProductId, pvo.VersionId, pvo.OperatingSystemId })
                .IsUnique();

            b.Entity<Ticket>()
                .HasIndex(t => new { t.StatusId, t.CreatedAt });

        }

    }
}
