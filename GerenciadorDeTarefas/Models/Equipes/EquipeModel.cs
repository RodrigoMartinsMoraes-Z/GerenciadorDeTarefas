using GerenciadorDeTarefas.Models.Tarefas;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorDeTarefas.Models.Equipes
{
    public class EquipeModel
    {
        public string Nome { get; set; }
        public ICollection<TarefaModel> Tarefas { get; set; }
    }
}
