using Microsoft.EntityFrameworkCore.Migrations;

namespace GerenciadorDeTarefas.Context.Migrations
{
    public partial class permissao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Permissao",
                table: "Usuarios",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Permissao",
                table: "Usuarios");
        }
    }
}
