

using GerenciadorDeTarefas.Domain.Equipes;
using GerenciadorDeTarefas.Domain.ManyToMany;
using GerenciadorDeTarefas.Domain.Objetivos;
using GerenciadorDeTarefas.Domain.Pessoas;
using GerenciadorDeTarefas.Domain.Projects;
using GerenciadorDeTarefas.Domain.Tasks;
using GerenciadorDeTarefas.Domain.Usuarios;

using Microsoft.EntityFrameworkCore;

using Starlight.Core.DbHelper;

namespace GerenciadorDeTarefas.Domain.Context
{
    public interface IContext : IDbContext
    {
        DbSet<Team> Teams { get; }
        DbSet<Objective> Objectives { get; }
        DbSet<Person> People { get; }
        DbSet<Project> Projects { get; }
        DbSet<Task> Tasks { get; }
        DbSet<User> Users { get; }
        DbSet<TeamUser> TeamUser { get; }
    }
}
