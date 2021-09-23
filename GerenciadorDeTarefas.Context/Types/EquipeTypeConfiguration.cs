using GerenciadorDeTarefas.Domain.Equipes;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeTarefas.Context.Types
{
    class EquipeTypeConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.Projects).WithOne(p => p.Equipe).HasForeignKey(p => p.IdEquipe);
        }
    }
}
