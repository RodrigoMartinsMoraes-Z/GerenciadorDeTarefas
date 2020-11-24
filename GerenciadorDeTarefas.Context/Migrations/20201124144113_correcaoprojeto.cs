using Microsoft.EntityFrameworkCore.Migrations;

namespace GerenciadorDeTarefas.Context.Migrations
{
    public partial class correcaoprojeto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projetos_Equipes_IdProjeto",
                table: "Projetos");

            migrationBuilder.RenameColumn(
                name: "IdProjeto",
                table: "Projetos",
                newName: "IdEquipe");

            migrationBuilder.RenameIndex(
                name: "IX_Projetos_IdProjeto",
                table: "Projetos",
                newName: "IX_Projetos_IdEquipe");

            migrationBuilder.AddForeignKey(
                name: "FK_Projetos_Equipes_IdEquipe",
                table: "Projetos",
                column: "IdEquipe",
                principalTable: "Equipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projetos_Equipes_IdEquipe",
                table: "Projetos");

            migrationBuilder.RenameColumn(
                name: "IdEquipe",
                table: "Projetos",
                newName: "IdProjeto");

            migrationBuilder.RenameIndex(
                name: "IX_Projetos_IdEquipe",
                table: "Projetos",
                newName: "IX_Projetos_IdProjeto");

            migrationBuilder.AddForeignKey(
                name: "FK_Projetos_Equipes_IdProjeto",
                table: "Projetos",
                column: "IdProjeto",
                principalTable: "Equipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
