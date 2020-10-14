using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using GerenciadorDeTarefas.Domain.Equipes;
using GerenciadorDeTarefas.Domain.ManyToMany;
using GerenciadorDeTarefas.Domain.Pessoas;

namespace GerenciadorDeTarefas.Domain.Usuarios
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Login { get; set; }

        private string Senha;

        public string GetSenha()
        {
            return Senha;
        }

        public void SetSenha(string value)
        {
            Senha = EncriptarSenha(value);
        }

        public Pessoa Pessoa{ get; set; }

        public virtual ICollection<EquipeUsuario> Equipes { get; set; }

        public string EncriptarSenha(string value)
        {
            byte[] salt = Encoding.UTF8.GetBytes(Login);
            byte[] senhaByte = Encoding.UTF8.GetBytes(value);
            byte[] sha256 = new SHA256Managed().ComputeHash(senhaByte.Concat(salt).ToArray());
            return Convert.ToBase64String(sha256);
        }
    }
}
