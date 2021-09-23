using GerenciadorDeTarefas.Domain.Objectives;
using GerenciadorDeTarefas.Domain.Projects;

using System;
using System.Collections.Generic;

namespace GerenciadorDeTarefas.Domain.Tasks
{
    public class Task
    {
        public Task()
        {
            SubTask = new List<Task>();
        }

        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int? FatherTaskId { get; set; }
        public int ObjectiveId { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public DateTime Added { get; set; }
        public DateTime? Finished { get; set; }
        public DateTime? Forecast { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }

        public virtual Project Project { get; set; }
        public virtual Task FatherTask { get; set; }
        public virtual Objective Objective { get; set; }

        public virtual ICollection<Task> SubTask { get; set; }

    }
}
