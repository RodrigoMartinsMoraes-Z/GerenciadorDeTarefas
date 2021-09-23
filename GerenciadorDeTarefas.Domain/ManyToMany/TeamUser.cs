

using GerenciadorDeTarefas.Domain.Teams;
using GerenciadorDeTarefas.Domain.Users;

namespace GerenciadorDeTarefas.Domain.ManyToMany
{
    public class TeamUser
    {
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public Role UserRole { get; set; }
    }
}
