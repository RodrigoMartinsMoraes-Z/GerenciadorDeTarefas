using GerenciadorDeTarefas.Domain.ManyToMany;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeTarefas.Context.Types.ManyToMany
{
    internal class TeamUserTypeConfiguration : IEntityTypeConfiguration<TeamUser>
    {
        public void Configure(EntityTypeBuilder<TeamUser> builder)
        {
            builder.HasKey(eu => new { eu.TeamId, eu.UserId });

            builder.HasOne(eu => eu.Team).WithMany(e => e.TeamUsers).HasForeignKey(eu => eu.TeamId);
            builder.HasOne(eu => eu.User).WithMany(e => e.Teams).HasForeignKey(eu => eu.UserId);
        }
    }
}
