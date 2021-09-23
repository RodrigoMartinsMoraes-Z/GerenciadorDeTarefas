using GerenciadorDeTarefas.Context.Types;
using GerenciadorDeTarefas.Context.Types.ManyToMany;
using GerenciadorDeTarefas.Domain.Context;
using GerenciadorDeTarefas.Domain.ManyToMany;
using GerenciadorDeTarefas.Domain.Objectives;
using GerenciadorDeTarefas.Domain.People;
using GerenciadorDeTarefas.Domain.Projects;
using GerenciadorDeTarefas.Domain.Tasks;
using GerenciadorDeTarefas.Domain.Teams;
using GerenciadorDeTarefas.Domain.Users;

using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeTarefas.Context
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext, IContext
    {
        public DbContext(DbContextOptions<DbContext> options) : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Objective> Objectives { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TeamUser> TeamUser { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Equipe
            builder.ApplyConfiguration(new TeamTypeConfiguration());
            builder.ApplyConfiguration(new TeamUserTypeConfiguration());

            //Pessoa
            builder.ApplyConfiguration(new PersonTypeConfiguration());
            builder.ApplyConfiguration(new UserTypeConfiguration());

            //Projeto
            builder.ApplyConfiguration(new ProjectTypeConfiguration());
            builder.ApplyConfiguration(new ObjectiveTypeConfiguration());
            builder.ApplyConfiguration(new TaskTypeConfiguration());
        }
    }
}
