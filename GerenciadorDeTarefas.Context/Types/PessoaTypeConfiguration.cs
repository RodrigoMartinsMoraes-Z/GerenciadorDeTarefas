using GerenciadorDeTarefas.Domain.Pessoas;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorDeTarefas.Context.Types
{
    public class PessoaTypeConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Email).IsUnique();
        }
    }
}
