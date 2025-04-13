using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lotofacil.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class correçãoematributodaentidadeBaseContest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TopTenNumbers",
                table: "BaseContest",
                type: "nvarchar(29)",
                maxLength: 29,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(29)",
                oldMaxLength: 29);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TopTenNumbers",
                table: "BaseContest",
                type: "nvarchar(29)",
                maxLength: 29,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(29)",
                oldMaxLength: 29,
                oldNullable: true);
        }
    }
}
