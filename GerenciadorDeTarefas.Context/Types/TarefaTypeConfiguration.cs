using GerenciadorDeTarefas.Domain.Tarefas;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeTarefas.Context.Types
{
    public class TarefaTypeConfiguration : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasMany(t => t.SubTarefas).WithOne(t => t.TarefaPrincipal).HasForeignKey(t => t.IdTarefaPrincipal);

            builder.HasOne(t => t.Objetivo).WithMany(t => t.Tarefas).HasForeignKey(t => t.IdObjetivo);
        }
    }
}
