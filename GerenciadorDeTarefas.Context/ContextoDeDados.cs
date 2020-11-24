using GerenciadorDeTarefas.Context.Types;
using GerenciadorDeTarefas.Context.Types.ManyToMany;
using GerenciadorDeTarefas.Domain.Contexto;
using GerenciadorDeTarefas.Domain.Equipes;
using GerenciadorDeTarefas.Domain.ManyToMany;
using GerenciadorDeTarefas.Domain.Objetivos;
using GerenciadorDeTarefas.Domain.Pessoas;
using GerenciadorDeTarefas.Domain.Projetos;
using GerenciadorDeTarefas.Domain.Tarefas;
using GerenciadorDeTarefas.Domain.Usuarios;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using Starlight.Core.DbHelper;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.Context
{
    public class ContextoDeDados : DbContext, IContextoDeDados
    {
        public ContextoDeDados(DbContextOptions<ContextoDeDados> options) : base(options)
        {
        }

        public DbSet<Equipe> Equipes { get; set; }
        public DbSet<Objetivo> Objetivos { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<EquipeUsuario> EquipeUsuario { get; set; }

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
