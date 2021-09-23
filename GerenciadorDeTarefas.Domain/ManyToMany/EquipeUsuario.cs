using GerenciadorDeTarefas.Domain.Equipes;
using GerenciadorDeTarefas.Domain.Usuarios;

namespace GerenciadorDeTarefas.Domain.ManyToMany
{
    public class EquipeUsuario
    {
        public int IdEquipe { get; set; }
        public virtual Equipe Equipe { get; set; }
        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }
        public Role PermissaoUsuario { get; set; }
    }
}
