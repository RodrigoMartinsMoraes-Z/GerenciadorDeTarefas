using GerenciadorDeTarefas.Common.Models.Projects;
using GerenciadorDeTarefas.Common.Models.Usuarios;

using System.Collections.Generic;

namespace GerenciadorDeTarefas.Common.Models.Teams
{
    public class TeamModel
    {
        public TeamModel()
        {
            Projects = new List<ProjectModel>();
            Users = new List<UserModel>();
        }

        public int Id { get; }
        public string Nome { get; set; }
        public ICollection<ProjectModel> Projects { get; set; }

        public ICollection<UserModel> Users { get; set; }
    }
}
