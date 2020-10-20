using GerenciadorDeTarefas.Domain.ManyToMany;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeTarefas.Context.Types.ManyToMany
{
    class EquipeUsuarioTypeConfiguration : IEntityTypeConfiguration<EquipeUsuario>
    {
        public void Configure(EntityTypeBuilder<EquipeUsuario> builder)
        {
            builder.HasKey(eu => new { eu.IdEquipe, eu.IdUsuario });

            builder.HasOne(eu => eu.Equipe).WithMany(e => e.Usuarios).HasForeignKey(eu => eu.IdEquipe);
            builder.HasOne(eu => eu.Usuario).WithMany(e => e.Equipes).HasForeignKey(eu => eu.IdUsuario);
        }
    }
}
