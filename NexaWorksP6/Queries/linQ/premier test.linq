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



    Tickets.Count().Dump(" 1/ Nombre total de tickets");
	
    Tickets
    .GroupBy(t => t.Product.Name)
    .Select(g => new { Produit = g.Key, Nb = g.Count() })
    .OrderByDescending(x => x.Nb)
    .Dump("2/ Tickets par produit");

	
		Tickets
    .GroupBy(t => t.OperatingSystem.Name)
    .Select(g => new {
        OS = g.Key,
        Total   = g.Count(),
        Resolus = g.Count(t => t.Status.Name == "Résolu"),
        EnCours = g.Count(t => t.Status.Name == "En cours")
    })
    .OrderByDescending(x => x.Total)
    .Dump("3/ Tickets par OS (détail)");


    Tickets
        .Where(t => t.Status.Name == "En cours")
        .OrderByDescending(t => t.CreatedAt)
        .Select(t => new { t.Id, Produit = t.Product.Name, OS = t.OperatingSystem.Name, t.Problem })
        .Dump("4/ Tickets en cours");
		

    Tickets
        .Where(t => t.Status.Name == "Résolu")
        .OrderByDescending(t => t.ResolvedAt)
        .Select(t => new { t.Id, Produit = t.Product.Name, t.Problem, t.Resolution })
        .Dump("5/ Tickets résolus");


    ProductVersionOs
        .Select(pvo => new { Produit = pvo.Product.Name, Version = pvo.Version.Name, OS = pvo.OperatingSystem.Name })
        .OrderBy(x => x.Produit).ThenBy(x => x.Version).ThenBy(x => x.OS)
        .Dump("6/ Compatibilités");



