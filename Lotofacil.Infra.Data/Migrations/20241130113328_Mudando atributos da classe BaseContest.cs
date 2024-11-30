using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lotofacil.Migrations
{
    /// <inheritdoc />
    public partial class MudandoatributosdaclasseBaseContest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Matched15",
                table: "BaseContest",
                newName: "Hit15");

            migrationBuilder.RenameColumn(
                name: "Matched14",
                table: "BaseContest",
                newName: "Hit14");

            migrationBuilder.RenameColumn(
                name: "Matched13",
                table: "BaseContest",
                newName: "Hit13");

            migrationBuilder.RenameColumn(
                name: "Matched12",
                table: "BaseContest",
                newName: "Hit12");

            migrationBuilder.RenameColumn(
                name: "Matched11",
                table: "BaseContest",
                newName: "Hit11");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Hit15",
                table: "BaseContest",
                newName: "Matched15");

            migrationBuilder.RenameColumn(
                name: "Hit14",
                table: "BaseContest",
                newName: "Matched14");

            migrationBuilder.RenameColumn(
                name: "Hit13",
                table: "BaseContest",
                newName: "Matched13");

            migrationBuilder.RenameColumn(
                name: "Hit12",
                table: "BaseContest",
                newName: "Matched12");

            migrationBuilder.RenameColumn(
                name: "Hit11",
                table: "BaseContest",
                newName: "Matched11");
        }
    }
}
