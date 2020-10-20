using GerenciadorDeTarefas.Context.Types;
using GerenciadorDeTarefas.Context.Types.ManyToMany;
using GerenciadorDeTarefas.Domain.Contexto;
using GerenciadorDeTarefas.Domain.Equipes;
using GerenciadorDeTarefas.Domain.Funcionalidades;
using GerenciadorDeTarefas.Domain.Pessoas;
using GerenciadorDeTarefas.Domain.Projetos;
using GerenciadorDeTarefas.Domain.Tarefas;
using GerenciadorDeTarefas.Domain.Usuarios;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorDeTarefas.Context
{
    public class ContextoDeDados : DbContext, IContextoDeDados
    {
        public ContextoDeDados(DbContextOptions<ContextoDeDados> options) : base(options)
        {
        }

        public DbSet<Equipe> Equipes { get; set; }
        public DbSet<Objetivo> Funcionalidades { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

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
