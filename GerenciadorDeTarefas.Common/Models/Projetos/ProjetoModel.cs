using GerenciadorDeTarefas.Common.Models.Equipes;
using GerenciadorDeTarefas.Common.Models.Objetivos;
using GerenciadorDeTarefas.Common.Models.Tarefas;

using System.Collections.Generic;

namespace GerenciadorDeTarefas.Common.Models.Projetos
{
    public class ProjetoModel
    {
        public ProjetoModel()
        {
            Funcionalidades = new List<ObjetivoModel>();
            Tarefas = new List<TarefaModel>();
        }
        public int Id { get; set; }
        public int IdProjeto { get; set; }
        public string Nome { get; set; }

        public virtual EquipeModel Equipe { get; set; }
        public virtual ICollection<ObjetivoModel> Funcionalidades { get; set; }
        public virtual ICollection<TarefaModel> Tarefas { get; set; }
    }
}
