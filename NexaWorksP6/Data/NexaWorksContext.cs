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
            b.Entity<ProductVersionOs>().HasKey(x => new { x.ProductId, x.VersionId, x.OperatingSystemId });

            b.Entity<Ticket>()
                .HasOne(t => t.Pvo)
                .WithMany(pvo => pvo.Tickets)
                .HasForeignKey(t => new { t.ProductId, t.VersionId, t.OperatingSystemId })
                .OnDelete(DeleteBehavior.Restrict);

            b.Entity<Ticket>().HasOne(t => t.Product)
                .WithMany(p => p.Tickets)
                .HasForeignKey(t => t.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            b.Entity<Ticket>().HasOne(t => t.Version)
                .WithMany(v => v.Tickets)
                .HasForeignKey(t => t.VersionId)
                .OnDelete(DeleteBehavior.Restrict);

            b.Entity<Ticket>().HasOne(t => t.OperatingSystem)
                .WithMany(o => o.Tickets)
                .HasForeignKey(t => t.OperatingSystemId)
                .OnDelete(DeleteBehavior.Restrict);

            b.Entity<Ticket>().HasOne(t => t.Status)
                .WithMany(s => s.Tickets)
                .HasForeignKey(t => t.StatusId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
