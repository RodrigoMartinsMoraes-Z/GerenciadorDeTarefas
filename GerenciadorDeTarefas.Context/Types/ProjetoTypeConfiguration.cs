using GerenciadorDeTarefas.Domain.Projetos;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorDeTarefas.Context.Types
{
    public class ProjetoTypeConfiguration : IEntityTypeConfiguration<Projeto>
    {
        public void Configure(EntityTypeBuilder<Projeto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasMany(p => p.Tarefas).WithOne(t => t.Projeto).HasForeignKey(p => p.Id);
            builder.HasMany(p => p.Funcionalidades).WithOne(t => t.Projeto).HasForeignKey(p => p.Id);
        }
    }
}
