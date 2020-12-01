using Microsoft.EntityFrameworkCore.Migrations;

namespace GerenciadorDeTarefas.Context.Migrations
{
    public partial class Permissao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Permissao",
                table: "Usuarios");

            migrationBuilder.AddColumn<int>(
                name: "PermissaoUsuario",
                table: "EquipeUsuario",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermissaoUsuario",
                table: "EquipeUsuario");

            migrationBuilder.AddColumn<int>(
                name: "Permissao",
                table: "Usuarios",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
