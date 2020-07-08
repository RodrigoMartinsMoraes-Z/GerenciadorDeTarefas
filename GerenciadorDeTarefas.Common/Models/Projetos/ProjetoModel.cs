using GerenciadorDeTarefas.Common.Models.Funcionalidades;
using GerenciadorDeTarefas.Common.Models.Tarefas;
using System.Collections.Generic;

namespace GerenciadorDeTarefas.Common.Models.Projetos
{
    public class ProjetoModel
    {
        public ProjetoModel()
        {
            Tarefas = new List<TarefaModel>();
        }

        public string Nome { get; set; }
        public ICollection<FuncionalidadeModel> Funcionalidades { get; set; }
        public ICollection<TarefaModel> Tarefas { get; set; }
    }
}
