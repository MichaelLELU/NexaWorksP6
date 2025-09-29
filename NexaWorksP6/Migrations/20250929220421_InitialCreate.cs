using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NexaWorksP6.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OperatingSystems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperatingSystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Versions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Versions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductVersionOs",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    VersionId = table.Column<int>(type: "int", nullable: false),
                    OperatingSystemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVersionOs", x => new { x.ProductId, x.VersionId, x.OperatingSystemId });
                    table.ForeignKey(
                        name: "FK_ProductVersionOs_OperatingSystems_OperatingSystemId",
                        column: x => x.OperatingSystemId,
                        principalTable: "OperatingSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductVersionOs_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductVersionOs_Versions_VersionId",
                        column: x => x.VersionId,
                        principalTable: "Versions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    VersionId = table.Column<int>(type: "int", nullable: false),
                    OperatingSystemId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResolvedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Problem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Resolution = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_ProductVersionOs_ProductId_VersionId_OperatingSystemId",
                        columns: x => new { x.ProductId, x.VersionId, x.OperatingSystemId },
                        principalTable: "ProductVersionOs",
                        principalColumns: new[] { "ProductId", "VersionId", "OperatingSystemId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "OperatingSystems",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Windows" },
                    { 2, "Linux" },
                    { 3, "MacOS" },
                    { 4, "Android" },
                    { 5, "iOS" },
                    { 6, "Windows Mobile" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Trader en Herbe" },
                    { 2, "Maître des Investissements" },
                    { 3, "Planificateur d’Entraînement" },
                    { 4, "Planificateur d’Anxiété Sociale" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "En cours" },
                    { 2, "Résolu" }
                });

            migrationBuilder.InsertData(
                table: "Versions",
                columns: new[] { "Id", "Name", "ProductId" },
                values: new object[,]
                {
                    { 1, "1.0", 1 },
                    { 2, "1.1", 1 },
                    { 3, "1.2", 1 },
                    { 4, "1.3", 1 },
                    { 5, "1.0", 2 },
                    { 6, "2.0", 2 },
                    { 7, "2.1", 2 },
                    { 8, "1.0", 3 },
                    { 9, "1.1", 3 },
                    { 10, "2.0", 3 },
                    { 11, "1.0", 4 },
                    { 12, "2.1", 4 }
                });

            migrationBuilder.InsertData(
                table: "ProductVersionOs",
                columns: new[] { "OperatingSystemId", "ProductId", "VersionId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 1 },
                    { 3, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 2 },
                    { 2, 1, 3 },
                    { 3, 1, 3 },
                    { 4, 1, 3 },
                    { 5, 1, 3 },
                    { 6, 1, 3 },
                    { 2, 1, 4 },
                    { 3, 1, 4 },
                    { 3, 2, 5 },
                    { 3, 2, 6 },
                    { 4, 2, 6 },
                    { 5, 2, 6 },
                    { 3, 2, 7 },
                    { 4, 2, 7 },
                    { 5, 2, 7 },
                    { 2, 3, 8 },
                    { 3, 3, 8 },
                    { 2, 3, 9 },
                    { 3, 3, 9 },
                    { 4, 3, 9 },
                    { 5, 3, 9 },
                    { 1, 3, 10 },
                    { 2, 3, 10 },
                    { 3, 3, 10 },
                    { 5, 3, 10 },
                    { 1, 4, 11 },
                    { 3, 4, 11 },
                    { 4, 4, 11 },
                    { 5, 4, 11 },
                    { 1, 4, 12 },
                    { 3, 4, 12 },
                    { 4, 4, 12 },
                    { 5, 4, 12 }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "CreatedAt", "OperatingSystemId", "Problem", "ProductId", "Resolution", "ResolvedAt", "StatusId", "VersionId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Crash aléatoire à l’ouverture du module Portefeuille (Linux)", 1, null, null, 1, 1 },
                    { 2, new DateTime(2023, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Graphiques n’affichent pas les mèches en échelle logarithmique (Windows)", 1, "Correction du moteur graphique", new DateTime(2023, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 },
                    { 3, new DateTime(2023, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Erreur de connexion API temps réel (MacOS)", 1, null, null, 1, 2 },
                    { 4, new DateTime(2023, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Affichage incorrect des cours sur mobile (Android)", 1, "Ajustement des layouts responsives", new DateTime(2023, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3 },
                    { 5, new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Synchronisation des alertes iOS non fiable", 1, null, null, 1, 3 },
                    { 6, new DateTime(2023, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Blocage sur import CSV (Linux)", 1, "Amélioration du parseur CSV", new DateTime(2023, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 4 },
                    { 7, new DateTime(2023, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Crash à l’export PDF (MacOS)", 2, null, null, 1, 5 },
                    { 8, new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Notifications push répétées (Android)", 2, "Filtrage des doublons", new DateTime(2023, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 6 },
                    { 9, new DateTime(2023, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Problème d’accès FaceID (iOS)", 2, null, null, 1, 6 },
                    { 10, new DateTime(2023, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Décalage fuseau horaire dans les rapports (MacOS)", 2, "Conversion timezone locale", new DateTime(2023, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 7 },
                    { 11, new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Lenteur sur grands portefeuilles (Android)", 2, null, null, 1, 7 },
                    { 12, new DateTime(2023, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Crash lors du partage via AirDrop (iOS)", 2, "Mise à jour librairie iOS", new DateTime(2023, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 7 },
                    { 13, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Synchronisation CalDAV échoue (Linux)", 3, null, null, 1, 8 },
                    { 14, new DateTime(2023, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Erreur de rendu calendrier (MacOS)", 3, "Correctif CSS + mise à jour moteur UI", new DateTime(2023, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 8 },
                    { 15, new DateTime(2023, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Widget Android ne se met pas à jour", 3, null, null, 1, 9 },
                    { 16, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Rappel iOS non déclenché", 3, "Ajustement du système de notifications locales", new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 9 },
                    { 17, new DateTime(2024, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Erreur à l’export Excel (Windows)", 3, null, null, 1, 10 },
                    { 18, new DateTime(2024, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Graphique calories ne s’affiche pas (MacOS)", 3, "Correction binding données", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 10 },
                    { 19, new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Impossible d’ajouter un nouvel événement (Windows)", 4, null, null, 1, 11 },
                    { 20, new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Mauvais affichage calendrier (MacOS)", 4, "Adaptation layout responsive", new DateTime(2024, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 11 },
                    { 21, new DateTime(2024, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Crash en ajoutant un rappel (Android)", 4, null, null, 1, 11 },
                    { 22, new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Notifications silencieuses sur iOS", 4, "Réglage des permissions push", new DateTime(2024, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 11 },
                    { 23, new DateTime(2024, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Erreur sauvegarde locale (Windows)", 4, null, null, 1, 12 },
                    { 24, new DateTime(2024, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Crash lors de l’export PDF (MacOS)", 4, "Mise à jour moteur PDF", new DateTime(2024, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 12 },
                    { 25, new DateTime(2024, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Synchronisation iCloud intermittente (iOS)", 4, null, null, 1, 12 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductVersionOs_OperatingSystemId",
                table: "ProductVersionOs",
                column: "OperatingSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVersionOs_ProductId_VersionId_OperatingSystemId",
                table: "ProductVersionOs",
                columns: new[] { "ProductId", "VersionId", "OperatingSystemId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductVersionOs_VersionId",
                table: "ProductVersionOs",
                column: "VersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ProductId_VersionId_OperatingSystemId",
                table: "Tickets",
                columns: new[] { "ProductId", "VersionId", "OperatingSystemId" });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_StatusId_CreatedAt",
                table: "Tickets",
                columns: new[] { "StatusId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Versions_ProductId_Name",
                table: "Versions",
                columns: new[] { "ProductId", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "ProductVersionOs");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "OperatingSystems");

            migrationBuilder.DropTable(
                name: "Versions");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
