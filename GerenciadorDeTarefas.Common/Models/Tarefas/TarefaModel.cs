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
        public string Nome { get; set; }
        public string Detalhes { get; set; }
        public DateTime Adicionado { get; set; }
        public DateTime? Finalizacao { get; set; }
        public DateTime? Previsao { get; set; }
        public Situacao Situacao { get; set; }
        public Prioridade Prioridade { get; set; }

        public ICollection<TarefaModel> SubTarefas { get; set; }
    }
}
