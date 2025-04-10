using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lotofacil.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CriaçãodenovocamponatabelaBaseContesteexclusãodecamponatabelaContest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastProcessedTopTenJob",
                table: "Contest");

            migrationBuilder.AddColumn<int>(
                name: "TotalProcessed",
                table: "BaseContest",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalProcessed",
                table: "BaseContest");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastProcessedTopTenJob",
                table: "Contest",
                type: "datetime2",
                nullable: true);
        }
    }
}
