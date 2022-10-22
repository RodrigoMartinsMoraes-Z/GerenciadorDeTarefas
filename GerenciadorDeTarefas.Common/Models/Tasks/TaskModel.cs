using GerenciadorDeTarefas.Common.Models.Objectives;
using GerenciadorDeTarefas.Common.Models.Projects;

using System;
using System.Collections.Generic;

namespace GerenciadorDeTarefas.Common.Models.Tarefas
{
    public class TaskModel
    {
        public TaskModel()
        {
            SubTask = new List<TaskModel>();
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

        public virtual ProjectModel Project { get; set; }
        public virtual TaskModel FatherTask { get; set; }
        public virtual ObjectiveModel Objective { get; set; }

        public virtual ICollection<TaskModel> SubTask { get; set; }
    }
}
