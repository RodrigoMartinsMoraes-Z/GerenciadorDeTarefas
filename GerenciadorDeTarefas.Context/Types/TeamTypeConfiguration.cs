
using GerenciadorDeTarefas.Domain.Teams;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeTarefas.Context.Types
{
    internal class TeamTypeConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.Projects).WithOne(p => p.Team).HasForeignKey(p => p.IdEquipe);
        }
    }
}
