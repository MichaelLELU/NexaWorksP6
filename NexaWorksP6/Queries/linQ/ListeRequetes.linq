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
</Query>


// ---- Paramètres que tu peux modifier pour tester ----

var debut   = new DateTime(2023, 1, 1);
var fin     = new DateTime(2025, 12, 31);

//var produit = "Trader en Herbe";
//var version = "1.0";
//string[] motsCles = { "Crash", "Erreur", "Sync" };

//var produit = "Maître des Investissements";
//var version = "2.1";
//string[] motsCles = { "Crash" };

var produit = "Planificateur d’Entraînement";
var version = "1.0";
string[] motsCles = { "Erreur" };

// ----------------------------------------------------
// 1) Obtenir tous les problèmes en cours (tous les produits)
Tickets
    .Where(t => t.Status.Name == "En cours")
    .OrderByDescending(t => t.CreatedAt)
    .Select(t => new {
        t.Id, Produit = t.Product.Name, Version = t.Version.Name,
        OS = t.OperatingSystem.Name, t.Problem, t.CreatedAt
    })
    .Dump("1) En cours — tous les produits");

// 2) En cours pour un produit (toutes les versions)
Tickets
    .Where(t => t.Status.Name == "En cours" && t.Product.Name == produit)
    .OrderByDescending(t => t.CreatedAt)
    .Select(t => new {
        t.Id, Produit = t.Product.Name, Version = t.Version.Name,
        OS = t.OperatingSystem.Name, t.Problem, t.CreatedAt
    })
    .Dump("2) En cours — produit (toutes versions)");

// 3) En cours pour un produit (une seule version)
Tickets
    .Where(t => t.Status.Name == "En cours" && t.Product.Name == produit && t.Version.Name == version)
    .OrderByDescending(t => t.CreatedAt)
    .Select(t => new {
        t.Id, Produit = t.Product.Name, Version = t.Version.Name,
        OS = t.OperatingSystem.Name, t.Problem, t.CreatedAt
    })
    .Dump("3) En cours — produit + version");

// 4) En cours sur une période pour un produit (toutes versions)
Tickets
    .Where(t => t.Status.Name == "En cours"
             && t.Product.Name == produit
             && t.CreatedAt >= debut && t.CreatedAt <= fin)
    .OrderByDescending(t => t.CreatedAt)
    .Select(t => new {
        t.Id, Produit = t.Product.Name, Version = t.Version.Name,
        OS = t.OperatingSystem.Name, t.Problem, t.CreatedAt
    })
    .Dump("4) En cours — période + produit");

// 5) En cours sur une période pour un produit (une version)
Tickets
    .Where(t => t.Status.Name == "En cours"
             && t.Product.Name == produit && t.Version.Name == version
             && t.CreatedAt >= debut && t.CreatedAt <= fin)
    .OrderByDescending(t => t.CreatedAt)
    .Select(t => new {
        t.Id, Produit = t.Product.Name, Version = t.Version.Name,
        OS = t.OperatingSystem.Name, t.Problem, t.CreatedAt
    })
    .Dump("5) En cours — période + produit + version");

// 6) En cours contenant une liste de mots-clés (tous produits)  [ANY]
Tickets
    .Where(t => t.Status.Name == "En cours"
             && motsCles.Any(kw => EF.Functions.Like(t.Problem, "%" + kw + "%")))
    .OrderByDescending(t => t.CreatedAt)
    .Select(t => new {
        t.Id, Produit = t.Product.Name, Version = t.Version.Name,
        OS = t.OperatingSystem.Name, t.Problem, t.CreatedAt
    })
    .Dump("6) En cours — mots-clés (tous produits)");

// 7) En cours + mots-clés pour un produit (toutes versions)
Tickets
    .Where(t => t.Status.Name == "En cours"
             && t.Product.Name == produit
             && motsCles.Any(kw => EF.Functions.Like(t.Problem, "%" + kw + "%")))
    .OrderByDescending(t => t.CreatedAt)
    .Select(t => new {
        t.Id, Produit = t.Product.Name, Version = t.Version.Name,
        OS = t.OperatingSystem.Name, t.Problem, t.CreatedAt
    })
    .Dump("7) En cours — mots-clés + produit");

// 8) En cours + mots-clés pour un produit (une version)
Tickets
    .Where(t => t.Status.Name == "En cours"
             && t.Product.Name == produit && t.Version.Name == version
             && motsCles.Any(kw => EF.Functions.Like(t.Problem, "%" + kw + "%")))
    .OrderByDescending(t => t.CreatedAt)
    .Select(t => new {
        t.Id, Produit = t.Product.Name, Version = t.Version.Name,
        OS = t.OperatingSystem.Name, t.Problem, t.CreatedAt
    })
    .Dump("8) En cours — mots-clés + produit + version");

// 9) En cours pendant une période + mots-clés — produit (toutes versions)
Tickets
    .Where(t => t.Status.Name == "En cours"
             && t.Product.Name == produit
             && t.CreatedAt >= debut && t.CreatedAt <= fin
             && motsCles.Any(kw => EF.Functions.Like(t.Problem, "%" + kw + "%")))
    .OrderByDescending(t => t.CreatedAt)
    .Select(t => new {
        t.Id, Produit = t.Product.Name, Version = t.Version.Name,
        OS = t.OperatingSystem.Name, t.Problem, t.CreatedAt
    })
    .Dump("9) En cours — période + mots-clés + produit");

// 10) En cours pendant une période + mots-clés — produit (une version)
Tickets
    .Where(t => t.Status.Name == "En cours"
             && t.Product.Name == produit && t.Version.Name == version
             && t.CreatedAt >= debut && t.CreatedAt <= fin
             && motsCles.Any(kw => EF.Functions.Like(t.Problem, "%" + kw + "%")))
    .OrderByDescending(t => t.CreatedAt)
    .Select(t => new {
        t.Id, Produit = t.Product.Name, Version = t.Version.Name,
        OS = t.OperatingSystem.Name, t.Problem, t.CreatedAt
    })
    .Dump("10) En cours — période + mots-clés + produit + version");

// ----------------------------------------------------
// 11) Obtenir tous les problèmes résolus (tous les produits)
Tickets
    .Where(t => t.Status.Name == "Résolu")
    .OrderByDescending(t => t.ResolvedAt)
    .Select(t => new {
        t.Id, Produit = t.Product.Name, Version = t.Version.Name,
        OS = t.OperatingSystem.Name, t.Problem, t.Resolution, t.ResolvedAt
    })
    .Dump("11) Résolus — tous les produits");

// 12) Résolus pour un produit (toutes les versions)
Tickets
    .Where(t => t.Status.Name == "Résolu" && t.Product.Name == produit)
    .OrderByDescending(t => t.ResolvedAt)
    .Select(t => new {
        t.Id, Produit = t.Product.Name, Version = t.Version.Name,
        OS = t.OperatingSystem.Name, t.Problem, t.Resolution, t.ResolvedAt
    })
    .Dump("12) Résolus — produit (toutes versions)");

// 13) Résolus pour un produit (une version)
Tickets
    .Where(t => t.Status.Name == "Résolu" && t.Product.Name == produit && t.Version.Name == version)
    .OrderByDescending(t => t.ResolvedAt)
    .Select(t => new {
        t.Id, Produit = t.Product.Name, Version = t.Version.Name,
        OS = t.OperatingSystem.Name, t.Problem, t.Resolution, t.ResolvedAt
    })
    .Dump("13) Résolus — produit + version");

// 14) Résolus au cours d’une période — produit (toutes versions)   // filtre sur ResolvedAt
Tickets
    .Where(t => t.Status.Name == "Résolu"
             && t.Product.Name == produit
             && t.ResolvedAt >= debut && t.ResolvedAt <= fin)
    .OrderByDescending(t => t.ResolvedAt)
    .Select(t => new {
        t.Id, Produit = t.Product.Name, Version = t.Version.Name,
        OS = t.OperatingSystem.Name, t.Problem, t.Resolution, t.ResolvedAt
    })
    .Dump("14) Résolus — période + produit");

// 15) Résolus au cours d’une période — produit (une version)
Tickets
    .Where(t => t.Status.Name == "Résolu"
             && t.Product.Name == produit && t.Version.Name == version
             && t.ResolvedAt >= debut && t.ResolvedAt <= fin)
    .OrderByDescending(t => t.ResolvedAt)
    .Select(t => new {
        t.Id, Produit = t.Product.Name, Version = t.Version.Name,
        OS = t.OperatingSystem.Name, t.Problem, t.Resolution, t.ResolvedAt
    })
    .Dump("15) Résolus — période + produit + version");

// 16) Résolus contenant une liste de mots-clés (tous les produits) [ANY]
Tickets
    .Where(t => t.Status.Name == "Résolu"
             && motsCles.Any(kw => EF.Functions.Like(t.Problem, "%" + kw + "%")))
    .OrderByDescending(t => t.ResolvedAt)
    .Select(t => new {
        t.Id, Produit = t.Product.Name, Version = t.Version.Name,
        OS = t.OperatingSystem.Name, t.Problem, t.Resolution, t.ResolvedAt
    })
    .Dump("16) Résolus — mots-clés (tous produits)");

// 17) Résolus + mots-clés — produit (toutes versions)
Tickets
    .Where(t => t.Status.Name == "Résolu"
             && t.Product.Name == produit
             && motsCles.Any(kw => EF.Functions.Like(t.Problem, "%" + kw + "%")))
    .OrderByDescending(t => t.ResolvedAt)
    .Select(t => new {
        t.Id, Produit = t.Product.Name, Version = t.Version.Name,
        OS = t.OperatingSystem.Name, t.Problem, t.Resolution, t.ResolvedAt
    })
    .Dump("17) Résolus — mots-clés + produit");

// 18) Résolus + mots-clés — produit (une version)
Tickets
    .Where(t => t.Status.Name == "Résolu"
             && t.Product.Name == produit && t.Version.Name == version
             && motsCles.Any(kw => EF.Functions.Like(t.Problem, "%" + kw + "%")))
    .OrderByDescending(t => t.ResolvedAt)
    .Select(t => new {
        t.Id, Produit = t.Product.Name, Version = t.Version.Name,
        OS = t.OperatingSystem.Name, t.Problem, t.Resolution, t.ResolvedAt
    })
    .Dump("18) Résolus — mots-clés + produit + version");

// 19) Résolus pendant une période + mots-clés — produit (toutes versions)
Tickets
    .Where(t => t.Status.Name == "Résolu"
             && t.Product.Name == produit
             && t.ResolvedAt >= debut && t.ResolvedAt <= fin
             && motsCles.Any(kw => EF.Functions.Like(t.Problem, "%" + kw + "%")))
    .OrderByDescending(t => t.ResolvedAt)
    .Select(t => new {
        t.Id, Produit = t.Product.Name, Version = t.Version.Name,
        OS = t.OperatingSystem.Name, t.Problem, t.Resolution, t.ResolvedAt
    })
    .Dump("19) Résolus — période + mots-clés + produit");

// 20) Résolus pendant une période + mots-clés — produit (une version)
Tickets
    .Where(t => t.Status.Name == "Résolu"
             && t.Product.Name == produit && t.Version.Name == version
             && t.ResolvedAt >= debut && t.ResolvedAt <= fin
             && motsCles.Any(kw => EF.Functions.Like(t.Problem, "%" + kw + "%")))
    .OrderByDescending(t => t.ResolvedAt)
    .Select(t => new {
        t.Id, Produit = t.Product.Name, Version = t.Version.Name,
        OS = t.OperatingSystem.Name, t.Problem, t.Resolution, t.ResolvedAt
    })
    .Dump("20) Résolus — période + mots-clés + produit + version");
