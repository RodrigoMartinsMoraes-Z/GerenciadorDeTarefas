
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
            Objetivos = new List<Objective>();
            Tarefas = new List<Task>();
        }
        public int Id { get; set; }
        public int IdEquipe { get; set; }
        public string Nome { get; set; }

        public virtual Team Equipe { get; set; }
        public virtual ICollection<Objective> Objetivos { get; set; }
        public virtual ICollection<Task> Tarefas { get; set; }
    }
}
