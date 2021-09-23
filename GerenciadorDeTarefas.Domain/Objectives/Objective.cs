using GerenciadorDeTarefas.Domain.Projects;
using GerenciadorDeTarefas.Domain.Tasks;

using System;
using System.Collections.Generic;

namespace GerenciadorDeTarefas.Domain.Objectives
{
    public class Objective
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public DateTime Added { get; set; }
        public DateTime? Finished { get; set; }
        public DateTime? Forecast { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }

        public virtual Project Project { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
