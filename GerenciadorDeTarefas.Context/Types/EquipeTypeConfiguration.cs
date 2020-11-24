using GerenciadorDeTarefas.Domain.Equipes;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeTarefas.Context.Types
{
    class EquipeTypeConfiguration : IEntityTypeConfiguration<Equipe>
    {
        public void Configure(EntityTypeBuilder<Equipe> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.Projetos).WithOne(p => p.Equipe).HasForeignKey(p => p.IdEquipe);
        }
    }
}
