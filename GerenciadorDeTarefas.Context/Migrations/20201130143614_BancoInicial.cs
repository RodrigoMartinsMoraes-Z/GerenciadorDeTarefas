using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GerenciadorDeTarefas.Context.Migrations
{
    public partial class BancoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    Nascimento = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projetos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdEquipe = table.Column<int>(type: "integer", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projetos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projetos_Equipes_IdEquipe",
                        column: x => x.IdEquipe,
                        principalTable: "Equipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdPessoa = table.Column<int>(type: "integer", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: true),
                    Senha = table.Column<string>(type: "text", nullable: true),
                    Permissao = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdPessoa);
                    table.ForeignKey(
                        name: "FK_Usuarios_Pessoas_IdPessoa",
                        column: x => x.IdPessoa,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Objetivos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdProjeto = table.Column<int>(type: "integer", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    Detalhes = table.Column<string>(type: "text", nullable: true),
                    Adicionado = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Finalizacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Previsao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Situacao = table.Column<int>(type: "integer", nullable: false),
                    Prioridade = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objetivos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Objetivos_Projetos_IdProjeto",
                        column: x => x.IdProjeto,
                        principalTable: "Projetos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipeUsuario",
                columns: table => new
                {
                    IdEquipe = table.Column<int>(type: "integer", nullable: false),
                    IdUsuario = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipeUsuario", x => new { x.IdEquipe, x.IdUsuario });
                    table.ForeignKey(
                        name: "FK_EquipeUsuario_Equipes_IdEquipe",
                        column: x => x.IdEquipe,
                        principalTable: "Equipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipeUsuario_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdPessoa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tarefas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdProjeto = table.Column<int>(type: "integer", nullable: false),
                    IdTarefaPrincipal = table.Column<int>(type: "integer", nullable: true),
                    IdObjetivo = table.Column<int>(type: "integer", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    Detalhes = table.Column<string>(type: "text", nullable: true),
                    Adicionado = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Finalizacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Previsao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Situacao = table.Column<int>(type: "integer", nullable: false),
                    Prioridade = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tarefas_Objetivos_IdObjetivo",
                        column: x => x.IdObjetivo,
                        principalTable: "Objetivos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tarefas_Projetos_IdProjeto",
                        column: x => x.IdProjeto,
                        principalTable: "Projetos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tarefas_Tarefas_IdTarefaPrincipal",
                        column: x => x.IdTarefaPrincipal,
                        principalTable: "Tarefas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipeUsuario_IdUsuario",
                table: "EquipeUsuario",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Objetivos_IdProjeto",
                table: "Objetivos",
                column: "IdProjeto");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_Email",
                table: "Pessoas",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projetos_IdEquipe",
                table: "Projetos",
                column: "IdEquipe");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_IdObjetivo",
                table: "Tarefas",
                column: "IdObjetivo");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_IdProjeto",
                table: "Tarefas",
                column: "IdProjeto");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_IdTarefaPrincipal",
                table: "Tarefas",
                column: "IdTarefaPrincipal");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Login",
                table: "Usuarios",
                column: "Login",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipeUsuario");

            migrationBuilder.DropTable(
                name: "Tarefas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Objetivos");

            migrationBuilder.DropTable(
                name: "Pessoas");

            migrationBuilder.DropTable(
                name: "Projetos");

            migrationBuilder.DropTable(
                name: "Equipes");
        }
    }
}
