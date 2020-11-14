using GerenciadorDeTarefas.Domain.Objetivos;
using GerenciadorDeTarefas.Domain.Projetos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.Domain.Tarefas
{
    public class Tarefa
    {
        public Tarefa()
        {
            SubTarefas = new List<Tarefa>();
        }

        public int Id { get; set; }
        public int IdProjeto { get; set; }
        public int? IdTarefaPrincipal { get; set; }
        public int IdObjetivo { get; set; }
        public string Nome { get; set; }
        public string Detalhes { get; set; }
        public DateTime Adicionado { get; set; }
        public DateTime? Finalizacao { get; set; }
        public DateTime? Previsao { get; set; }
        public Situacao Situacao { get; set; }
        public Prioridade Prioridade { get; set; }

        public virtual Projeto Projeto { get; set; }
        public virtual Tarefa TarefaPrincipal { get; set; }
        public virtual Objetivo Objetivo { get; set; }

        public virtual ICollection<Tarefa> SubTarefas { get; set; }

    }
}
