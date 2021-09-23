using GerenciadorDeTarefas.Domain.Projects;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeTarefas.Context.Types
{
    public class ProjectTypeConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasMany(p => p.Tasks).WithOne(t => t.Project).HasForeignKey(p => p.ProjectId);
            builder.HasMany(p => p.Objectives).WithOne(t => t.Project).HasForeignKey(p => p.ProjectId);

        }
    }
}
