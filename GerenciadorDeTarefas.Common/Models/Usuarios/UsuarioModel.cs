using GerenciadorDeTarefas.Common.Models.Equipes;

using System.Collections.Generic;

namespace GerenciadorDeTarefas.Common.Models.Usuarios
{
    public class UsuarioModel
    {
        public UsuarioModel()
        {
            Equipes = new List<EquipeModel>();
        }

        public ICollection<EquipeModel> Equipes { get; set; }

    }
}
