using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lotofacil.Migrations
{
    public partial class ConcursosBaseadicionamconcuroscommaisde11pontos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConcursoBaseId",
                table: "Concursos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Concursos_ConcursoBaseId",
                table: "Concursos",
                column: "ConcursoBaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Concursos_ConcursosBase_ConcursoBaseId",
                table: "Concursos",
                column: "ConcursoBaseId",
                principalTable: "ConcursosBase",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Concursos_ConcursosBase_ConcursoBaseId",
                table: "Concursos");

            migrationBuilder.DropIndex(
                name: "IX_Concursos_ConcursoBaseId",
                table: "Concursos");

            migrationBuilder.DropColumn(
                name: "ConcursoBaseId",
                table: "Concursos");
        }
    }
}
