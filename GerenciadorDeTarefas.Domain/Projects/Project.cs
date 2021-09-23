
using GerenciadorDeTarefas.Domain.Objectives;
using GerenciadorDeTarefas.Domain.Tasks;
using GerenciadorDeTarefas.Domain.Teams;

using System.Collections.Generic;

namespace GerenciadorDeTarefas.Domain.Projects
{
    public class Project
    {
        public Project()
        {
            Objectives = new List<Objective>();
            Tasks = new List<Task>();
        }
        public int Id { get; set; }
        public int IdEquipe { get; set; }
        public string Name { get; set; }

        public virtual Team Team { get; set; }
        public virtual ICollection<Objective> Objectives { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
