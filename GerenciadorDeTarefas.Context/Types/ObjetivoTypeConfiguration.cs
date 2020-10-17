using GerenciadorDeTarefas.Domain.Funcionalidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorDeTarefas.Context.Types
{
    class ObjetivoTypeConfiguration : IEntityTypeConfiguration<Objetivo>
    {
        public void Configure(EntityTypeBuilder<Objetivo> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.Tarefas);
        }
    }
}
