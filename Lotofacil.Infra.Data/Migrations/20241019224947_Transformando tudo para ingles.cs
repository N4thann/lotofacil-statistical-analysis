using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lotofacil.Migrations
{
    public partial class Transformandotudoparaingles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Concursos");

            migrationBuilder.DropTable(
                name: "ConcursosBase");

            migrationBuilder.CreateTable(
                name: "BaseContests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Matched11 = table.Column<int>(type: "int", nullable: false),
                    Matched12 = table.Column<int>(type: "int", nullable: false),
                    Matched13 = table.Column<int>(type: "int", nullable: false),
                    Matched14 = table.Column<int>(type: "int", nullable: false),
                    Matched15 = table.Column<int>(type: "int", nullable: false),
                    BaseNumbers = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseContests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DrawnNumbers = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    BaseContestId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contests_BaseContests_BaseContestId",
                        column: x => x.BaseContestId,
                        principalTable: "BaseContests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contests_BaseContestId",
                table: "Contests",
                column: "BaseContestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contests");

            migrationBuilder.DropTable(
                name: "BaseContests");

            migrationBuilder.CreateTable(
                name: "ConcursosBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Acertou11 = table.Column<int>(type: "int", nullable: false),
                    Acertou12 = table.Column<int>(type: "int", nullable: false),
                    Acertou13 = table.Column<int>(type: "int", nullable: false),
                    Acertou14 = table.Column<int>(type: "int", nullable: false),
                    Acertou15 = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    NumerosConcursoBase = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConcursosBase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Concursos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConcursoBaseId = table.Column<int>(type: "int", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    NumerosConcurso = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concursos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Concursos_ConcursosBase_ConcursoBaseId",
                        column: x => x.ConcursoBaseId,
                        principalTable: "ConcursosBase",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Concursos_ConcursoBaseId",
                table: "Concursos",
                column: "ConcursoBaseId");
        }
    }
}
