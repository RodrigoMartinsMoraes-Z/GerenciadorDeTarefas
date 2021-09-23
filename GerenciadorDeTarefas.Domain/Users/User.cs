using GerenciadorDeTarefas.Domain.ManyToMany;
using GerenciadorDeTarefas.Domain.Pessoas;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace GerenciadorDeTarefas.Domain.Users
{
    public class User
    {
        private string _pass;

        public User()
        {
            Team = new List<TeamUser>();
        }

        public int PersonId { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Pass { get => _pass; set => _pass = EncryptPass(value); }
        public virtual Person Person { get; set; }

        public virtual ICollection<TeamUser> Team { get; set; }

        private string EncryptPass(string value)
        {
            byte[] salt = Encoding.UTF8.GetBytes(Login);
            byte[] senhaByte = Encoding.UTF8.GetBytes(value);
            byte[] sha256 = new SHA256Managed().ComputeHash(senhaByte.Concat(salt).ToArray());
            return Convert.ToBase64String(sha256);
        }
    }
}
