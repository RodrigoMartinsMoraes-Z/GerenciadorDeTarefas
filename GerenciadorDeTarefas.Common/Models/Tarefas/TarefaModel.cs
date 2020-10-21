using GerenciadorDeTarefas.Common.Models.Objetivos;
using GerenciadorDeTarefas.Common.Models.Projetos;

using System;
using System.Collections.Generic;

namespace GerenciadorDeTarefas.Common.Models.Tarefas
{
    public class TarefaModel
    {
        public TarefaModel()
        {
            SubTarefas = new List<TarefaModel>();
        }

        public int Id { get; set; }
        public int IdProjeto { get; set; }
        public int IdTarefaPrincipal { get; set; }
        public int IdObjetivo { get; set; }
        public string Nome { get; set; }
        public string Detalhes { get; set; }
        public DateTime Adicionado { get; set; }
        public DateTime? Finalizacao { get; set; }
        public DateTime? Previsao { get; set; }
        public Situacao Situacao { get; set; }
        public Prioridade Prioridade { get; set; }

        public virtual ProjetoModel Projeto { get; set; }
        public virtual TarefaModel TarefaPrincipal { get; set; }
        public virtual ObjetivoModel Objetivo { get; set; }

        public ICollection<TarefaModel> SubTarefas { get; set; }
    }
}
