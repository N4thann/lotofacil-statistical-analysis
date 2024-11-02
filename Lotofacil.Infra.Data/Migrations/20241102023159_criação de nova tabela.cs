using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lotofacil.Migrations
{
    /// <inheritdoc />
    public partial class criaçãodenovatabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DrawnNumbers",
                table: "Contests",
                newName: "Numbers");

            migrationBuilder.RenameColumn(
                name: "BaseNumbers",
                table: "BaseContests",
                newName: "Numbers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Numbers",
                table: "Contests",
                newName: "DrawnNumbers");

            migrationBuilder.RenameColumn(
                name: "Numbers",
                table: "BaseContests",
                newName: "BaseNumbers");
        }
    }
}
