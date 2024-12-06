using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lotofacil.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CorringindoentidadeContestActivityLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchedAnyBaseContest",
                table: "ContestActivityLog");

            migrationBuilder.AlterColumn<string>(
                name: "BaseContestNumbers",
                table: "ContestActivityLog",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BaseContestName",
                table: "ContestActivityLog",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BaseContestNumbers",
                table: "ContestActivityLog",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45);

            migrationBuilder.AlterColumn<string>(
                name: "BaseContestName",
                table: "ContestActivityLog",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<bool>(
                name: "MatchedAnyBaseContest",
                table: "ContestActivityLog",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
