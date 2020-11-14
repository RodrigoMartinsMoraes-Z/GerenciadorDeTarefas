using GerenciadorDeTarefas.Domain.Equipes;
using GerenciadorDeTarefas.Domain.Objetivos;
using GerenciadorDeTarefas.Domain.Pessoas;
using GerenciadorDeTarefas.Domain.Projetos;
using GerenciadorDeTarefas.Domain.Tarefas;
using GerenciadorDeTarefas.Domain.Usuarios;

using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeTarefas.Domain.Contexto
{
    public interface IContextoDeDados
    {
        DbSet<Equipe> Equipes { get; }
        DbSet<Objetivo> Objetivos { get; }
        DbSet<Pessoa> Pessoas { get; }
        DbSet<Projeto> Projetos { get; }
        DbSet<Tarefa> Tarefas { get; }
        DbSet<Usuario> Usuarios { get; }
    }
}
