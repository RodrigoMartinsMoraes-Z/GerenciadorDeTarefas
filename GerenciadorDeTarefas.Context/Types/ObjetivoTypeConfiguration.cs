using GerenciadorDeTarefas.Domain.Objetivos;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeTarefas.Context.Types
{
    class ObjetivoTypeConfiguration : IEntityTypeConfiguration<Objective>
    {
        public void Configure(EntityTypeBuilder<Objective> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.Tarefas).WithOne(t => t.Objective).HasForeignKey(e => e.ObjectiveId); ;
        }
    }
}
