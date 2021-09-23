using GerenciadorDeTarefas.Domain.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeTarefas.Context.Types
{
    public class TarefaTypeConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasMany(t => t.SubTask).WithOne(t => t.FatherTask).HasForeignKey(t => t.FatherTaskId);

            builder.HasOne(t => t.Objective).WithMany(t => t.Tarefas).HasForeignKey(t => t.ObjectiveId);
        }
    }
}
