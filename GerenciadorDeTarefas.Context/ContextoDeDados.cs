using GerenciadorDeTarefas.Context.Types;
using GerenciadorDeTarefas.Context.Types.ManyToMany;
using GerenciadorDeTarefas.Domain.Contexto;
using GerenciadorDeTarefas.Domain.Equipes;
using GerenciadorDeTarefas.Domain.ManyToMany;
using GerenciadorDeTarefas.Domain.Objetivos;
using GerenciadorDeTarefas.Domain.Pessoas;
using GerenciadorDeTarefas.Domain.Projects;
using GerenciadorDeTarefas.Domain.Tasks;
using GerenciadorDeTarefas.Domain.Usuarios;

using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeTarefas.Context
{
    public class ContextoDeDados : DbContext, IContext
    {
        public ContextoDeDados(DbContextOptions<ContextoDeDados> options) : base(options)
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
            builder.ApplyConfiguration(new EquipeTypeConfiguration());
            builder.ApplyConfiguration(new EquipeUsuarioTypeConfiguration());

            //Pessoa
            builder.ApplyConfiguration(new PessoaTypeConfiguration());
            builder.ApplyConfiguration(new UsuarioTypeConfiguration());

            //Projeto
            builder.ApplyConfiguration(new ProjetoTypeConfiguration());
            builder.ApplyConfiguration(new ObjetivoTypeConfiguration());
            builder.ApplyConfiguration(new TarefaTypeConfiguration());
        }
    }
}
