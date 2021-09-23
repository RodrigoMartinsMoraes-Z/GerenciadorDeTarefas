using GerenciadorDeTarefas.Domain.Users;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerenciadorDeTarefas.Context.Types
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.PersonId);

            builder.HasOne(u => u.Person).WithMany().HasForeignKey(u => u.PersonId);

            builder.HasIndex(p => p.Email).IsUnique();
            builder.HasIndex(u => u.Login).IsUnique();

        }
    }
}
