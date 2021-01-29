using GerenciadorDeTarefas.Common.Models.Projetos;
using GerenciadorDeTarefas.Common.Models.Usuarios;

using System.Collections.Generic;

namespace GerenciadorDeTarefas.Common.Models.Equipes
{
    public class EquipeModel
    {
        public EquipeModel()
        {
            Projetos = new List<ProjetoModel>();
            Usuarios = new List<UsuarioModel>();
        }

        public int Id { get; }
        public string Nome { get; set; }
        public ICollection<ProjetoModel> Projetos { get; set; }

        public ICollection<UsuarioModel> Usuarios { get; set; }
    }
}
