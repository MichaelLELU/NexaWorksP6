<Query Kind="Statements">
  <Connection>
    <ID>80b53fe1-c34a-4da0-979d-74fbd2c5dcf2</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="EF7Driver" PublicKeyToken="469b5aa5a4331a8c">EF7Driver.StaticDriver</Driver>
    <CustomAssemblyPath>C:\Repos\OPENCLASSROOM\NexaWorksP6\NexaWorksP6\bin\Debug\net8.0\NexaWorksP6.dll</CustomAssemblyPath>
    <CustomTypeName>NexaWorksP6.Data.NexaWorksContext</CustomTypeName>
    <CustomCxString>Server=localhost\SQLEXPRESS;Database=NexaWorks;Trusted_Connection=True;TrustServerCertificate=True</CustomCxString>
    <DriverData>
      <UseDbContextOptions>true</UseDbContextOptions>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
  <Namespace>System.Globalization</Namespace>
</Query>

// ========= PARAMS =========
DateTime d1 = new(2023,1,1);
DateTime d2 = new(2025,12,31);         // inclusif via < d2.AddDays(1)
string status = "En cours";            // ou "Résolu"
int? productId = null;                 // filtres optionnels
int? versionId = null;
int? osId = null;
string q = "Erreur";                   // mot-clé
int days = 14;                         // seuil backlog
int top = 10;                          // TOP N
int slaDays = 7;                       // SLA en jours
var now = DateTime.UtcNow;


// 1) Total de tickets (période)
var q1 = Tickets
    .Where(t => t.CreatedAt >= d1 && t.CreatedAt < d2.AddDays(1))
    .Count();
q1.Dump("1/ Total tickets");


// 2) Tickets par produit
var q2 = Tickets
    .Where(t => t.CreatedAt >= d1 && t.CreatedAt < d2.AddDays(1))
    .Where(t => productId == null || t.ProductId == productId)
    .GroupBy(t => t.Pvo.Product.Name)
    .Select(g => new { Produit = g.Key, Nb = g.Count() })
    .OrderByDescending(x => x.Nb)
    .ToList();
q2.Dump("2/ Tickets par produit");


// 3) Tickets par OS avec répartition statut
var q3 = Tickets
    .Where(t => t.CreatedAt >= d1 && t.CreatedAt < d2.AddDays(1))
    .GroupBy(t => t.Pvo.OperatingSystem.Name)
    .Select(g => new {
        OS = g.Key,
        Resolus = g.Count(t => t.Status.Name == "Résolu"),
        EnCours = g.Count(t => t.Status.Name == "En cours"),
        Total   = g.Count()
    })
    .OrderByDescending(x => x.Total)
    .ToList();
q3.Dump("3/ Tickets par OS");


// 4) Backlog “En cours” détaillé (filtres optionnels)
var q4 = Tickets
    .Where(t => t.Status.Name == status)
    .Where(t => productId == null || t.ProductId == productId)
    .Where(t => versionId == null || t.VersionId == versionId)
    .Where(t => osId == null || t.OperatingSystemId == osId)
    .OrderByDescending(t => t.CreatedAt)
    .Select(t => new {
        t.Id,
        Produit = t.Pvo.Product.Name,
        Version = t.Pvo.Version.Name,
        OS      = t.Pvo.OperatingSystem.Name,
        t.CreatedAt
    })
    .ToList();
q4.Dump("4/ Backlog détaillé");


// 5) Backlog “En cours” depuis > X jours
var q5 = Tickets
    .Where(t => t.Status.Name == "En cours")
    .Where(t => EF.Functions.DateDiffDay(t.CreatedAt, now) > days)
    .OrderBy(t => t.CreatedAt)
    .Select(t => new {
        t.Id,
        Produit = t.Pvo.Product.Name,
        Version = t.Pvo.Version.Name,
        OS      = t.Pvo.OperatingSystem.Name,
        t.CreatedAt
    })
    .ToList();
q5.Dump("5/ Backlog > X jours");


// 6) Throughput mensuel (résolus / mois) — 100% EF Core
var q6 = Tickets
    .Where(t => t.Status.Name == "Résolu" && t.ResolvedAt >= d1 && t.ResolvedAt < d2.AddDays(1))
    .GroupBy(t => new { Annee = t.ResolvedAt.Value.Year, Mois = t.ResolvedAt.Value.Month })
    .Select(g => new { g.Key.Annee, g.Key.Mois, Resolus = g.Count() })
    .OrderBy(x => x.Annee).ThenBy(x => x.Mois)
    .ToList();
q6.Dump("6/ Résolus par mois");


// 7) Durée moyenne de résolution par produit (heures)
var q7 = Tickets
    .Where(t => t.Status.Name == "Résolu" && t.ResolvedAt >= d1 && t.ResolvedAt < d2.AddDays(1))
    .GroupBy(t => t.Pvo.Product.Name)
    .Select(g => new {
        Produit = g.Key,
        DureeH_Moy = g.Average(t => EF.Functions.DateDiffHour(t.CreatedAt, t.ResolvedAt))
    })
    .OrderBy(x => x.DureeH_Moy)
    .ToList();
q7.Dump("7/ Durée moyenne (h) par produit");


// 8) Médiane (approx) de résolution par OS (heures) — calcule en mémoire
var q8 = Tickets
    .Where(t => t.Status.Name == "Résolu" && t.ResolvedAt >= d1 && t.ResolvedAt < d2.AddDays(1))
    .Select(t => new { OS = t.Pvo.OperatingSystem.Name, H = EF.Functions.DateDiffHour(t.CreatedAt, t.ResolvedAt) })
    .AsEnumerable() // passage mémoire pour médiane
    .GroupBy(x => x.OS)
    .Select(g => {
        var arr = g.Select(x => x.H ?? 0).OrderBy(v => v).ToArray();
        var mid = arr.Length / 2;
        var p50 = arr.Length == 0 ? 0
                 : (arr.Length % 2 == 1 ? arr[mid] : (arr[mid - 1] + arr[mid]) / 2.0);
        return new { OS = g.Key, P50H = p50 };
    })
    .OrderBy(x => x.OS)
    .ToList();
q8.Dump("8/ Médiane (h) par OS");


// 9) Top N versions les plus “buggées”
var q9 = Tickets
    .Where(t => t.CreatedAt >= d1 && t.CreatedAt < d2.AddDays(1))
    .GroupBy(t => new { Prod = t.Pvo.Product.Name, Ver = t.Pvo.Version.Name })
    .Select(g => new { g.Key.Prod, g.Key.Ver, Nb = g.Count() })
    .OrderByDescending(x => x.Nb).ThenBy(x => x.Prod).ThenBy(x => x.Ver)
    .Take(top)
    .ToList();
q9.Dump("9/ Top N versions buggées");


// 10) Recherche texte problème/résolution
var q10 = Tickets
    .Where(t => t.CreatedAt >= d1 && t.CreatedAt < d2.AddDays(1))
    .Where(t => t.Problem.Contains(q) || (t.Resolution ?? "").Contains(q))
    .OrderByDescending(t => t.CreatedAt)
    .Select(t => new {
        t.Id,
        Produit = t.Pvo.Product.Name,
        Version = t.Pvo.Version.Name,
        OS      = t.Pvo.OperatingSystem.Name,
        t.CreatedAt
    })
    .ToList();
q10.Dump("10/ Recherche texte");


// 11) SLA : résolus hors délai
var q11 = Tickets
    .Where(t => t.Status.Name == "Résolu" && t.ResolvedAt >= d1 && t.ResolvedAt < d2.AddDays(1))
    .Where(t => EF.Functions.DateDiffDay(t.CreatedAt, t.ResolvedAt) > slaDays)
    .Select(t => new {
        t.Id,
        Produit = t.Pvo.Product.Name,
        Version = t.Pvo.Version.Name,
        OS      = t.Pvo.OperatingSystem.Name,
        Jours   = EF.Functions.DateDiffDay(t.CreatedAt, t.ResolvedAt)
    })
    .ToList();
q11.Dump("11/ SLA résolus dépassés");


// 12) SLA : en cours dépassés
var q12 = Tickets
    .Where(t => t.Status.Name == "En cours")
    .Where(t => EF.Functions.DateDiffDay(t.CreatedAt, now) > slaDays)
    .Select(t => new {
        t.Id,
        Produit = t.Pvo.Product.Name,
        Version = t.Pvo.Version.Name,
        OS      = t.Pvo.OperatingSystem.Name,
        Jours   = EF.Functions.DateDiffDay(t.CreatedAt, now)
    })
    .ToList();
q12.Dump("12/ SLA en cours dépassés");


// 13) Répartition par statut (période)
var q13 = Tickets
    .Where(t => t.CreatedAt >= d1 && t.CreatedAt < d2.AddDays(1))
    .GroupBy(t => t.Status.Name)
    .Select(g => new { Statut = g.Key, Nb = g.Count() })
    .ToList();
q13.Dump("13/ Répartition par statut");


// 14) Tickets filtrés par produitid > versionid > osid 
var q14 = Tickets
    .Where(t => productId == null || t.ProductId == productId)
    .Where(t => versionId == null || t.VersionId == versionId)
    .Where(t => osId == null || t.OperatingSystemId == osId)
    .ToList();
q14.Dump("14/ Tickets par triplet");


// 15) Combinaisons PVO sans aucun ticket
var q15 = ProductVersionOs
    .Where(pvo => !Tickets.Any(t =>
        t.ProductId == pvo.ProductId
        && t.VersionId == pvo.VersionId
        && t.OperatingSystemId == pvo.OperatingSystemId))
    .Select(pvo => new {
        Produit = pvo.Product.Name,
        Version = pvo.Version.Name,
        OS      = pvo.OperatingSystem.Name
    })
    .OrderBy(x => x.Produit).ThenBy(x => x.Version).ThenBy(x => x.OS)
    .ToList();

q15.Dump("15/ PVO sans tickets");

