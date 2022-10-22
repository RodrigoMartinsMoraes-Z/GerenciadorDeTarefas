using GerenciadorDeTarefas.Common.Models.Pessoas;
using GerenciadorDeTarefas.Common.Models.Teams;

using System.Collections.Generic;

namespace GerenciadorDeTarefas.Common.Models.Usuarios
{
    public class UserModel
    {
        public UserModel()
        {
            Person = new PersonModel();
            Teams = new List<TeamModel>();
        }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public Role? Role { get; set; }

        public virtual PersonModel Person { get; set; }
        public virtual ICollection<TeamModel> Teams { get; set; }

    }
}
