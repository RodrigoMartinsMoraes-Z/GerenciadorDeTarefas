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

            builder.HasOne(u => u.Pessoa).WithMany().HasForeignKey(u => u.IdPessoa);

            builder.HasIndex(p => p.Email).IsUnique();
            builder.HasIndex(u => u.Login).IsUnique();

        }
    }
}
