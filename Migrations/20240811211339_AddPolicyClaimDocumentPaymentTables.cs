using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AstreeWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddPolicyClaimDocumentPaymentTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Claim",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateSinistre = table.Column<DateOnly>(type: "date", nullable: false),
                    TypeSinistre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Montant = table.Column<float>(type: "real", nullable: false),
                    ClaimStatut = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Montant = table.Column<float>(type: "real", nullable: false),
                    DatePaiment = table.Column<DateOnly>(type: "date", nullable: false),
                    ModeOfPayment = table.Column<int>(type: "int", nullable: false),
                    PayStatut = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Policy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    DateDeb = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Prime = table.Column<int>(type: "int", nullable: false),
                    PolicStatut = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policy", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Claim");

            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Policy");
        }
    }
}
