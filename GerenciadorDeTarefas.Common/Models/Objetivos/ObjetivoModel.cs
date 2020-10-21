using GerenciadorDeTarefas.Common.Models.Projetos;
using GerenciadorDeTarefas.Common.Models.Tarefas;

using System;
using System.Collections.Generic;

namespace GerenciadorDeTarefas.Common.Models.Objetivos
{
    public class ObjetivoModel
    {
        public int Id { get; set; }
        public int IdProjeto { get; set; }
        public string Nome { get; set; }
        public string Detalhes { get; set; }
        public DateTime Adicionado { get; set; }
        public DateTime? Finalizacao { get; set; }
        public DateTime? Previsao { get; set; }
        public Situacao Situacao { get; set; }
        public Prioridade Prioridade { get; set; }

        public virtual ProjetoModel Projeto { get; set; }
        public virtual ICollection<TarefaModel> Tarefas { get; set; }
    }
}
