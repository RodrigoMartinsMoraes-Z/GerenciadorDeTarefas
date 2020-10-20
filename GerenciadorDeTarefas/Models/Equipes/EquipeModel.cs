using GerenciadorDeTarefas.Models.Projetos;
using GerenciadorDeTarefas.Models.Usuarios;

using System.Collections.Generic;

namespace GerenciadorDeTarefas.Models.Equipes
{
    public class EquipeModel
    {
        public EquipeModel()
        {
            Projetos = new List<ProjetoModel>();
        }

        public string Nome { get; set; }
        public ICollection<ProjetoModel> Projetos { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
