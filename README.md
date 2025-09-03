# NexaWorks — BDD, Seeds & Requêtes LINQ

Projet d’exemple pour **NexaWorks** : modélisation, création et alimentation d’une base **SQL Server** via **EF Core (Code-First)**, plus un pack de **requêtes LINQ** (exécutables en console et dans **LINQPad**).

## Sommaire
- [Aperçu](#aperçu)
- [Technologies utilisées](#technologies-utilisées)
- [Modèle de données (ERD)](#modèle-de-données-erd)
- [Structure du projet](#structure-du-projet)
- [Démarrage rapide](#démarrage-rapide)
  - [Prérequis](#prérequis)
  - [Configuration](#configuration)
  - [Créer la base et insérer les données (migrations + seed)](#créer-la-base-et-insérer-les-données-migrations--seed)
  - [Lancer la console](#lancer-la-console)
  - [Exécuter les requêtes dans LINQPad](#exécuter-les-requêtes-dans-linqpad)
- [Entités & design](#entités--design)
- [Requêtes demandées (20 cas)](#requêtes-demandées-20-cas)
- [Dump / Sauvegarde de la BDD](#dump--sauvegarde-de-la-bdd)

## Aperçu
- **Objectif :** disposer d’une base **SQL Server** avec **25 tickets réalistes**, couvrant plusieurs **produits**, **versions** et **OS**, et un set de **requêtes LINQ** pour l’équipe API.
- **Approche :** EF Core **Code-First** + **seed** (catalogues + compatibilités + tickets).
- **Livrables clés :**
  - Schéma EF Core + Migrations.
  - Données seedées (produits/versions/OS/statuts + 25 tickets).
  - Requêtes LINQ (pack LINQPad + exemples console).
  - Dump complet de la base (`.bak`).

## Technologies utilisées
- **.NET 8** (C#)  
- **Entity Framework Core 8** (migrations, seed)  
- **SQL Server** (Express ou LocalDB)  
- **LINQ / LINQPad 7+** (exécution & documentation des requêtes)  
- **Visual Studio 2022** / **VS Code**

**Packages NuGet principaux :**
- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Design`
- `Microsoft.Extensions.Configuration.Json` (lecture d’`appsettings.json`)

## Modèle de données (ERD)

<img width="1172" height="519" alt="image" src="https://github.com/user-attachments/assets/3e2a8304-2565-4e45-b5ca-2d953a543154" />


**Idée clé :** un **Ticket** référence une **combinaison valide** *(Produit, Version, OS)* via la table d’association **`ProductVersionOs`** (clé composite). Le **Statut** est en 1-N.

## Structure du projet
```
NexaWorksP6/
├─ NexaWorksP6.csproj
├─ appsettings.json
├─ Program.cs
├─ Data/
│  ├─ NexaWorksContext.cs
│  ├─ DesignTimeDbContextFactory.cs
│  └─ Seeds/
│     ├─ CatalogSeed.cs          # Produits, Versions, OS, Statuts
│     ├─ PvoSeed.cs              # Compatibilités Produit-Version-OS
│     └─ TicketSeed.cs           # 25 tickets
├─ Entities/
│  ├─ Product.cs
│  ├─ VersionEntity.cs
│  ├─ OperatingSystemEntity.cs   # (évite le conflit avec System.OperatingSystem)
│  ├─ Status.cs
│  ├─ ProductVersionOs.cs
│  └─ Ticket.cs
├─ Migrations/                    # généré par EF (à versionner)
└─ Linq/
   ├─ NexaWorks_Demo.linq
   └─ NexaWorks_AllQueries.linq   # 20 requêtes du scénario
```

## Démarrage rapide

### Prérequis
- **.NET SDK 8**
- **SQL Server** (Express / LocalDB) + **SSMS** (facultatif)
- **LINQPad 7/8** (facultatif mais recommandé)

### Configuration
#### 1) `appsettings.json`
À la racine du projet (au même niveau que le `.csproj`) :
```json
{
  "ConnectionStrings": {
    "NexaWorks": "Server=localhost\\SQLEXPRESS;Database=NexaWorks;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```
> Adapte le serveur si besoin : `(localdb)\\MSSQLLocalDB`, `localhost`, etc.


### Créer la base et insérer les données (migrations + seed)
```bash
dotnet restore
dotnet tool install --global dotnet-ef   # si besoin
dotnet ef migrations add InitWithSeeds
dotnet ef database update
```
> Les **InsertData** (seed) sont générés dans les migrations.  
> Si tu modifies les seeds, crée une **nouvelle migration**.

### Lancer la console
`Program.cs` applique les migrations au démarrage et exécute des requêtes de démo.
```bash
dotnet run
```

### Exécuter les requêtes dans LINQPad
1. **Add connection** → **Entity Framework Core (3.x → 9.x)**.  
2. **Build from: Project** → pointe le dossier du `.csproj`  
   *(ou)* **Typed DbContext in custom assembly** → `bin/Debug/net8.0/NexaWorksP6.dll`.  
3. **DbContext** : `NexaWorksP6.Data.NexaWorksContext`.  
4. **Use IDesignTimeDbContextFactory** : ✔️  
5. **Test** → **OK**.  
6. Ouvre `Linq/NexaWorks_AllQueries.linq` → **Run (F5)**.

## Entités & design
- **Product** *(Id, Name)*
- **VersionEntity** *(Id, Name)*
- **OperatingSystemEntity** *(Id, Name)*
- **Status** *(Id, Name)* — ex. *En cours*, *Résolu*
- **ProductVersionOs** *(ProductId, VersionId, OperatingSystemId)* — **clé composite**, référence **Product**, **VersionEntity**, **OperatingSystemEntity**
- **Ticket**
  - FK vers **Product / VersionEntity / OperatingSystemEntity** **et** vers **ProductVersionOs** (garantit une combinaison déclarée compatible)
  - Champs : *CreatedAt, ResolvedAt?, StatusId, Problem, Resolution?*

**Index conseillés :**
- `Ticket (ProductId, VersionId, OperatingSystemId)`
- `Ticket (StatusId)`
- Uniques sur `Product.Name`, `OperatingSystemEntity.Name`

## Requêtes demandées (20 cas)
Les 20 requêtes du scénario sont fournies dans **`Linq/NexaWorks_AllQueries.linq`**.  
Elles couvrent :
- **Problèmes “En cours”** : tous / par produit / par version / par période / **mots-clés** (`EF.Functions.Like`) et combinaisons.
- **Problèmes “Résolus”** : mêmes déclinaisons, filtrage temporel sur `ResolvedAt`.
- Sélection retournée : `Id, Produit, Version, OS, Statut, CreatedAt, ResolvedAt, Problem, Resolution`.


## Dump / Sauvegarde de la BDD
Créer une **sauvegarde complète** via **SSMS** :
1. Clic droit sur la base **NexaWorks** → **Tasks** → **Back Up…**
2. **Backup type** : *Full*, **Destination** : `.bak`
3. Dépose le fichier dans le repo (ex. `db-dumps/NexaWorks-full.bak`) et mentionne-le ici.

Restaurer : **Databases** → **Restore Database…** → sélectionner le `.bak`.

