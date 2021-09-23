using GerenciadorDeTarefas.Domain.Projects;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeTarefas.Context.Types
{
    public class ProjetoTypeConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasMany(p => p.Tarefas).WithOne(t => t.Project).HasForeignKey(p => p.ProjectId);
            builder.HasMany(p => p.Objetivos).WithOne(t => t.Project).HasForeignKey(p => p.ProjectId);

        }
    }
}
