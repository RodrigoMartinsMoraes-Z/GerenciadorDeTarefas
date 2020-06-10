using GerenciadorDeTarefas.Models.Projetos;
using GerenciadorDeTarefas.Models.Tarefas;
using GerenciadorDeTarefas.Models.Usuarios;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

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
