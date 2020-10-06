using GerenciadorDeTarefas.Domain.Equipes;
using GerenciadorDeTarefas.Domain.Funcionalidades;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeTarefas.Domain.Contexto
{
    public interface IContextoDeDados
    {
         DbSet<Equipe> Equipes { get; }
         DbSet<Funcionalidade> Funcionalidades { get; }
    }
}
