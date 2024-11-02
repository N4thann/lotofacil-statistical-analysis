using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lotofacil.Migrations
{
    public partial class Criaçãodobancodedados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Concursos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumerosConcurso = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concursos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConcursosBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Acertou11 = table.Column<int>(type: "int", nullable: false),
                    Acertou12 = table.Column<int>(type: "int", nullable: false),
                    Acertou13 = table.Column<int>(type: "int", nullable: false),
                    Acertou14 = table.Column<int>(type: "int", nullable: false),
                    Acertou15 = table.Column<int>(type: "int", nullable: false),
                    NumerosConcursoBase = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConcursosBase", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Concursos");

            migrationBuilder.DropTable(
                name: "ConcursosBase");
        }
    }
}
