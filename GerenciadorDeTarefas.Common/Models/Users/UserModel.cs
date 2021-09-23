using GerenciadorDeTarefas.Common.Models.People;
using GerenciadorDeTarefas.Common.Models.Teams;
using GerenciadorDeTarefas.Common.Users;

using System.Collections.Generic;

namespace GerenciadorDeTarefas.Common.Models.Users
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
        public string Senha { get; set; }
        public string Token { get; set; }
        public Role? Permissao { get; set; }

        public virtual PersonModel Person { get; set; }
        public virtual ICollection<TeamModel> Teams { get; set; }

    }
}
