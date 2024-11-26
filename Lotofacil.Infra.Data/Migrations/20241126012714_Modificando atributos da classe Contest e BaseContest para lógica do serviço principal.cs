using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lotofacil.Migrations
{
    /// <inheritdoc />
    public partial class ModificandoatributosdaclasseContesteBaseContestparalógicadoserviçoprincipal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Processed",
                table: "Contest");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastProcessed",
                table: "Contest",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "BaseContest",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastProcessed",
                table: "Contest");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "BaseContest");

            migrationBuilder.AddColumn<bool>(
                name: "Processed",
                table: "Contest",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
