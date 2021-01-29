using GerenciadorDeTarefas.Common.Models.Equipes;
using GerenciadorDeTarefas.Common.Models.Pessoas;

using System.Collections.Generic;

namespace GerenciadorDeTarefas.Common.Models.Usuarios
{
    public class UsuarioModel
    {
        public UsuarioModel()
        {
            Pessoa = new PessoaModel();
            Equipes = new List<EquipeModel>();
        }
        public string Login { get; set; }
        public string Senha { get; set; }
        public Permissao? Permissao { get; set; }
        public string Token { get; set; }

        public virtual PessoaModel Pessoa { get; set; }
        public virtual ICollection<EquipeModel> Equipes { get; set; }

    }
}
