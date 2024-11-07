using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lotofacil.Migrations
{
    /// <inheritdoc />
    public partial class Criaçãonovatabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contests_BaseContests_BaseContestId",
                table: "Contests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contests",
                table: "Contests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseContests",
                table: "BaseContests");

            migrationBuilder.RenameTable(
                name: "Contests",
                newName: "Contest");

            migrationBuilder.RenameTable(
                name: "BaseContests",
                newName: "BaseContest");

            migrationBuilder.RenameIndex(
                name: "IX_Contests_BaseContestId",
                table: "Contest",
                newName: "IX_Contest_BaseContestId");

            migrationBuilder.AlterColumn<string>(
                name: "Numbers",
                table: "Contest",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Contest",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Numbers",
                table: "BaseContest",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BaseContest",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contest",
                table: "Contest",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseContest",
                table: "BaseContest",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ContestActivityLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchedAnyBaseContest = table.Column<bool>(type: "bit", nullable: false),
                    BaseContestName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    BaseContestNumbers = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Numbers = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContestActivityLog", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Contest_BaseContest_BaseContestId",
                table: "Contest",
                column: "BaseContestId",
                principalTable: "BaseContest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contest_BaseContest_BaseContestId",
                table: "Contest");

            migrationBuilder.DropTable(
                name: "ContestActivityLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contest",
                table: "Contest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseContest",
                table: "BaseContest");

            migrationBuilder.RenameTable(
                name: "Contest",
                newName: "Contests");

            migrationBuilder.RenameTable(
                name: "BaseContest",
                newName: "BaseContests");

            migrationBuilder.RenameIndex(
                name: "IX_Contest_BaseContestId",
                table: "Contests",
                newName: "IX_Contests_BaseContestId");

            migrationBuilder.AlterColumn<string>(
                name: "Numbers",
                table: "Contests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Contests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Numbers",
                table: "BaseContests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BaseContests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contests",
                table: "Contests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseContests",
                table: "BaseContests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contests_BaseContests_BaseContestId",
                table: "Contests",
                column: "BaseContestId",
                principalTable: "BaseContests",
                principalColumn: "Id");
        }
    }
}
