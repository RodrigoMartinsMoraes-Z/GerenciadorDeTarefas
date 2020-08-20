using GerenciadorDeTarefas.Domain.Equipes;
using GerenciadorDeTarefas.Domain.Usuarios;

namespace GerenciadorDeTarefas.Domain.ManyToMany
{
    public class EquipeUsuario
    {
        public int IdEquipe { get; set; }
        public Equipe Equipe { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
    }
}
