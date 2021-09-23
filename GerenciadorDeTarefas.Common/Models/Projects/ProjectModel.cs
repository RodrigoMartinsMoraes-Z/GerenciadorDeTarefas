using GerenciadorDeTarefas.Common.Models.Objectives;
using GerenciadorDeTarefas.Common.Models.Tasks;
using GerenciadorDeTarefas.Common.Models.Teams;

using System.Collections.Generic;

namespace GerenciadorDeTarefas.Common.Models.Projects
{
    public class ProjectModel
    {
        public ProjectModel()
        {
            Objectives = new List<Objectives.ObjectiveModel>();
            Tasks = new List<TaskModel>();
        }
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }

        public virtual TeamModel Team { get; set; }
        public virtual ICollection<ObjectiveModel> Objectives { get; set; }
        public virtual ICollection<TaskModel> Tasks { get; set; }
    }
}
