using GerenciadorDeTarefas.Models.Tarefas;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorDeTarefas.Models.Projetos
{
    public class ProjetoModel
    {
        public ProjetoModel()
        {
            Tarefas = new List<TarefaModel>();
        }

        public string Nome { get; set; }
        public ICollection<TarefaModel> Tarefas { get; set; }
    }
}
