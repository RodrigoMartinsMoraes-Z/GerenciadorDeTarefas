using GerenciadorDeTarefas.Domain.Objectives;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeTarefas.Context.Types
{
    class ObjectiveTypeConfiguration : IEntityTypeConfiguration<Objective>
    {
        public void Configure(EntityTypeBuilder<Objective> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.Tasks).WithOne(t => t.Objective).HasForeignKey(e => e.ObjectiveId); ;
        }
    }
}
