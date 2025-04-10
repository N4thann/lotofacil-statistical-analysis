using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lotofacil.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CriandoealterandoatributosdaentidadeContest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastProcessed",
                table: "Contest",
                newName: "LastProcessedTopTenJob");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastProcessedMainJob",
                table: "Contest",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastProcessedMainJob",
                table: "Contest");

            migrationBuilder.RenameColumn(
                name: "LastProcessedTopTenJob",
                table: "Contest",
                newName: "LastProcessed");
        }
    }
}
