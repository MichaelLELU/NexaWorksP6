using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexaWorksP6.Data;

namespace NexaWorksP6.Demo
{
    public static class QueriesDemo
    {
        // 1. Compter le nombre total de tickets
        public static async Task ShowTicketsCount(NexaWorksContext db)
        {
            var count = await db.Tickets.CountAsync();
            Console.WriteLine($"\n--- Nombre total de tickets : {count} ---");
        }

        // 2. Lister les tickets en cours
        public static async Task ShowTicketsEnCours(NexaWorksContext db)
        {
            var enCours = await db.Tickets
                .Where(t => t.Status.Name == "En cours")
                .OrderByDescending(t => t.CreatedAt)
                .Select(t => new { t.Id, Produit = t.Product.Name, OS = t.OperatingSystem.Name, t.Problem })
                .ToListAsync();

            Console.WriteLine("\n--- Tickets en cours ---");
            foreach (var t in enCours)
                Console.WriteLine($"#{t.Id} [{t.Produit} - {t.OS}] {t.Problem}");
        }

        // 3. Afficher les 5 derniers tickets résolus
        public static async Task ShowDerniersResolus(NexaWorksContext db)
        {
            var derniers = await db.Tickets
                .Where(t => t.Status.Name == "Résolu")
                .OrderByDescending(t => t.ResolvedAt)
                .Take(5)
                .Select(t => new { t.Id, Produit = t.Product.Name, t.Problem, t.Resolution })
                .ToListAsync();

            Console.WriteLine("\n--- 5 derniers tickets résolus ---");
            foreach (var t in derniers)
                Console.WriteLine($"#{t.Id} {t.Produit} : {t.Problem} -> {t.Resolution}");
        }

        // 4. Nombre de tickets par produit
        public static async Task ShowTicketsParProduit(NexaWorksContext db)
        {
            var grouped = await db.Tickets
                .GroupBy(t => t.Product.Name)
                .Select(g => new { Produit = g.Key, Nb = g.Count() })
                .OrderByDescending(x => x.Nb)
                .ToListAsync();

            Console.WriteLine("\n--- Nombre de tickets par produit ---");
            foreach (var x in grouped)
                Console.WriteLine($"{x.Produit} : {x.Nb}");
        }

        // 5. Recherche par mot-clé dans les problèmes
        public static async Task SearchTickets(NexaWorksContext db, string keyword)
        {
            var results = await db.Tickets
                .Where(t => EF.Functions.Like(t.Problem, $"%{keyword}%"))
                .Select(t => new { t.Id, Produit = t.Product.Name, t.Problem })
                .ToListAsync();

            Console.WriteLine($"\n--- Tickets contenant '{keyword}' ---");
            foreach (var t in results)
                Console.WriteLine($"#{t.Id} [{t.Produit}] {t.Problem}");
        }

        // 6. Pagination
        public static async Task ShowTicketsPage(NexaWorksContext db, int pageIndex, int pageSize)
        {
            var page = await db.Tickets
                .OrderByDescending(t => t.CreatedAt)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new { t.Id, Produit = t.Product.Name, t.Problem })
                .ToListAsync();

            Console.WriteLine($"\n--- Page {pageIndex} ({pageSize} tickets) ---");
            foreach (var t in page)
                Console.WriteLine($"#{t.Id} {t.Produit} : {t.Problem}");
        }

        // 7. Compatibilités Produit-Version-OS
        public static async Task ShowCompatibilites(NexaWorksContext db)
        {
            var compat = await db.ProductVersionOs
                .Select(pvo => new { Produit = pvo.Product.Name, Version = pvo.Version.Name, OS = pvo.OperatingSystem.Name })
                .OrderBy(x => x.Produit).ThenBy(x => x.Version).ThenBy(x => x.OS)
                .ToListAsync();

            Console.WriteLine("\n--- Compatibilités Produit-Version-OS ---");
            foreach (var x in compat)
                Console.WriteLine($"{x.Produit} v{x.Version} -> {x.OS}");
        }

        // 8. Voir le SQL généré par EF Core (debug)
        public static void ShowSqlExample(NexaWorksContext db)
        {
            var sql = db.Tickets
                .Where(t => t.Status.Name == "En cours")
                .ToQueryString();

            Console.WriteLine("\n--- SQL généré pour les tickets en cours ---");
            Console.WriteLine(sql);
        }
    }
}
