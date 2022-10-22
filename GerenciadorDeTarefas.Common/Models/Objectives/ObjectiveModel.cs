using GerenciadorDeTarefas.Common.Models.Projects;
using GerenciadorDeTarefas.Common.Models.Tarefas;

using System;
using System.Collections.Generic;

namespace GerenciadorDeTarefas.Common.Models.Objectives
{
    public class ObjectiveModel
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

        public virtual ProjectModel Project { get; set; }
        public virtual ICollection<TaskModel> Tasks { get; set; }
    }
}
