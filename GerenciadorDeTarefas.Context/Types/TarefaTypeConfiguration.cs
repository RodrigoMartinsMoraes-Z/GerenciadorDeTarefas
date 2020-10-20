using GerenciadorDeTarefas.Domain.Tarefas;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GerenciadorDeTarefas.Context.Types
{
    public class TarefaTypeConfiguration : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasMany(t => t.SubTarefas).WithOne(t => t.TarefaPrincipal).HasForeignKey(t => t.IdTarefaPrincipal);
        }
    }
}
