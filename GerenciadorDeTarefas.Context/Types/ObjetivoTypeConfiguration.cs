using GerenciadorDeTarefas.Domain.Objetivos;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeTarefas.Context.Types
{
    class ObjetivoTypeConfiguration : IEntityTypeConfiguration<Objetivo>
    {
        public void Configure(EntityTypeBuilder<Objetivo> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.Tarefas).WithOne(t => t.Objetivo).HasForeignKey(e => e.IdObjetivo); ;
        }
    }
}
