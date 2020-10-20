using GerenciadorDeTarefas.Domain.Pessoas;
using GerenciadorDeTarefas.Domain.Usuarios;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeTarefas.Context.Types
{
    public class UsuarioTypeConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.IdPessoa);

            builder.HasOne(u => u.Pessoa).WithOne(p => p.Usuario).HasForeignKey<Usuario>(u => u.IdPessoa);

            builder.HasIndex(u => u.Login).IsUnique();
        }
    }
}
