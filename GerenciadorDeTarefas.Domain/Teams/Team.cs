using GerenciadorDeTarefas.Domain.ManyToMany;
using GerenciadorDeTarefas.Domain.Projects;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.Domain.Teams
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<TeamUser> TeamUsers { get; set; }

        public Task Validate()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
                throw new System.Exception("O campo 'Nome' deve ser preenchido.");

            return Task.CompletedTask;
        }
    }
}
