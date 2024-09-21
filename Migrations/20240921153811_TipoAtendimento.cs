using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BCCAlunos2024.Migrations
{
    /// <inheritdoc />
    public partial class TipoAtendimento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "situacao",
                table: "Salas",
                type: "nvarchar(1)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "tipo",
                table: "Atendimentos",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tipo",
                table: "Atendimentos");

            migrationBuilder.AlterColumn<int>(
                name: "situacao",
                table: "Salas",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)");
        }
    }
}
