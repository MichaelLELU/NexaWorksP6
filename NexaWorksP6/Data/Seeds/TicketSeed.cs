using Microsoft.EntityFrameworkCore;
using NexaWorksP6.Entities;

namespace NexaWorksP6.Data.Seeds
{
    public static class TicketSeed
    {
        public static void SeedTickets(ModelBuilder b)
        {
            b.Entity<Ticket>().HasData(
                // --- Trader en Herbe ---
                new Ticket { Id = 1, ProductId = 1, VersionId = 1, OperatingSystemId = 2, CreatedAt = new DateTime(2023, 1, 12), StatusId = 1, Problem = "Crash aléatoire à l’ouverture du module Portefeuille (Linux)", Resolution = null },
                new Ticket { Id = 2, ProductId = 1, VersionId = 1, OperatingSystemId = 1, CreatedAt = new DateTime(2023, 2, 5), ResolvedAt = new DateTime(2023, 2, 28), StatusId = 2, Problem = "Graphiques n’affichent pas les mèches en échelle logarithmique (Windows)", Resolution = "Correction du moteur graphique" },
                new Ticket { Id = 3, ProductId = 1, VersionId = 2, OperatingSystemId = 3, CreatedAt = new DateTime(2023, 3, 10), StatusId = 1, Problem = "Erreur de connexion API temps réel (MacOS)", Resolution = null },
                new Ticket { Id = 4, ProductId = 1, VersionId = 3, OperatingSystemId = 4, CreatedAt = new DateTime(2023, 4, 22), ResolvedAt = new DateTime(2023, 4, 30), StatusId = 2, Problem = "Affichage incorrect des cours sur mobile (Android)", Resolution = "Ajustement des layouts responsives" },
                new Ticket { Id = 5, ProductId = 1, VersionId = 3, OperatingSystemId = 5, CreatedAt = new DateTime(2023, 5, 15), StatusId = 1, Problem = "Synchronisation des alertes iOS non fiable", Resolution = null },
                new Ticket { Id = 6, ProductId = 1, VersionId = 4, OperatingSystemId = 2, CreatedAt = new DateTime(2023, 6, 8), ResolvedAt = new DateTime(2023, 6, 18), StatusId = 2, Problem = "Blocage sur import CSV (Linux)", Resolution = "Amélioration du parseur CSV" },

                // --- Maître des Investissements ---
                new Ticket { Id = 7, ProductId = 2, VersionId = 1, OperatingSystemId = 3, CreatedAt = new DateTime(2023, 7, 12), StatusId = 1, Problem = "Crash à l’export PDF (MacOS)", Resolution = null },
                new Ticket { Id = 8, ProductId = 2, VersionId = 5, OperatingSystemId = 4, CreatedAt = new DateTime(2023, 8, 1), ResolvedAt = new DateTime(2023, 8, 14), StatusId = 2, Problem = "Notifications push répétées (Android)", Resolution = "Filtrage des doublons" },
                new Ticket { Id = 9, ProductId = 2, VersionId = 5, OperatingSystemId = 5, CreatedAt = new DateTime(2023, 8, 20), StatusId = 1, Problem = "Problème d’accès FaceID (iOS)", Resolution = null },
                new Ticket { Id = 10, ProductId = 2, VersionId = 6, OperatingSystemId = 3, CreatedAt = new DateTime(2023, 9, 3), ResolvedAt = new DateTime(2023, 9, 25), StatusId = 2, Problem = "Décalage fuseau horaire dans les rapports (MacOS)", Resolution = "Conversion timezone locale" },
                new Ticket { Id = 11, ProductId = 2, VersionId = 6, OperatingSystemId = 4, CreatedAt = new DateTime(2023, 9, 15), StatusId = 1, Problem = "Lenteur sur grands portefeuilles (Android)", Resolution = null },
                new Ticket { Id = 12, ProductId = 2, VersionId = 6, OperatingSystemId = 5, CreatedAt = new DateTime(2023, 9, 29), ResolvedAt = new DateTime(2023, 10, 5), StatusId = 2, Problem = "Crash lors du partage via AirDrop (iOS)", Resolution = "Mise à jour librairie iOS" },

                // --- Planificateur d’Entraînement ---
                new Ticket { Id = 13, ProductId = 3, VersionId = 1, OperatingSystemId = 2, CreatedAt = new DateTime(2023, 11, 10), StatusId = 1, Problem = "Synchronisation CalDAV échoue (Linux)", Resolution = null },
                new Ticket { Id = 14, ProductId = 3, VersionId = 1, OperatingSystemId = 3, CreatedAt = new DateTime(2023, 11, 20), ResolvedAt = new DateTime(2023, 12, 5), StatusId = 2, Problem = "Erreur de rendu calendrier (MacOS)", Resolution = "Correctif CSS + mise à jour moteur UI" },
                new Ticket { Id = 15, ProductId = 3, VersionId = 2, OperatingSystemId = 4, CreatedAt = new DateTime(2023, 12, 15), StatusId = 1, Problem = "Widget Android ne se met pas à jour", Resolution = null },
                new Ticket { Id = 16, ProductId = 3, VersionId = 2, OperatingSystemId = 5, CreatedAt = new DateTime(2024, 1, 8), ResolvedAt = new DateTime(2024, 1, 20), StatusId = 2, Problem = "Rappel iOS non déclenché", Resolution = "Ajustement du système de notifications locales" },
                new Ticket { Id = 17, ProductId = 3, VersionId = 5, OperatingSystemId = 1, CreatedAt = new DateTime(2024, 2, 2), StatusId = 1, Problem = "Erreur à l’export Excel (Windows)", Resolution = null },
                new Ticket { Id = 18, ProductId = 3, VersionId = 5, OperatingSystemId = 3, CreatedAt = new DateTime(2024, 2, 18), ResolvedAt = new DateTime(2024, 3, 1), StatusId = 2, Problem = "Graphique calories ne s’affiche pas (MacOS)", Resolution = "Correction binding données" },

                // --- Planificateur d’Anxiété Sociale ---
                new Ticket { Id = 19, ProductId = 4, VersionId = 1, OperatingSystemId = 1, CreatedAt = new DateTime(2024, 3, 12), StatusId = 1, Problem = "Impossible d’ajouter un nouvel événement (Windows)", Resolution = null },
                new Ticket { Id = 20, ProductId = 4, VersionId = 1, OperatingSystemId = 3, CreatedAt = new DateTime(2024, 3, 20), ResolvedAt = new DateTime(2024, 3, 28), StatusId = 2, Problem = "Mauvais affichage calendrier (MacOS)", Resolution = "Adaptation layout responsive" },
                new Ticket { Id = 21, ProductId = 4, VersionId = 1, OperatingSystemId = 4, CreatedAt = new DateTime(2024, 4, 5), StatusId = 1, Problem = "Crash en ajoutant un rappel (Android)", Resolution = null },
                new Ticket { Id = 22, ProductId = 4, VersionId = 1, OperatingSystemId = 5, CreatedAt = new DateTime(2024, 4, 15), ResolvedAt = new DateTime(2024, 4, 30), StatusId = 2, Problem = "Notifications silencieuses sur iOS", Resolution = "Réglage des permissions push" },
                new Ticket { Id = 23, ProductId = 4, VersionId = 6, OperatingSystemId = 1, CreatedAt = new DateTime(2024, 5, 1), StatusId = 1, Problem = "Erreur sauvegarde locale (Windows)", Resolution = null },
                new Ticket { Id = 24, ProductId = 4, VersionId = 6, OperatingSystemId = 3, CreatedAt = new DateTime(2024, 5, 12), ResolvedAt = new DateTime(2024, 5, 25), StatusId = 2, Problem = "Crash lors de l’export PDF (MacOS)", Resolution = "Mise à jour moteur PDF" },
                new Ticket { Id = 25, ProductId = 4, VersionId = 6, OperatingSystemId = 5, CreatedAt = new DateTime(2024, 6, 2), StatusId = 1, Problem = "Synchronisation iCloud intermittente (iOS)", Resolution = null }
            );
        }
    }
}
