using GerenciadorDeTarefas.Domain.Equipes;
using GerenciadorDeTarefas.Domain.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorDeTarefas.Context.Types
{
    class EquipeTypeConfiguration : IEntityTypeConfiguration<Equipe>
    {
        public void Configure(EntityTypeBuilder<Equipe> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.Projetos).WithOne(p => p.Equipe).HasForeignKey(p => p.IdProjeto);
        }
    }
}
