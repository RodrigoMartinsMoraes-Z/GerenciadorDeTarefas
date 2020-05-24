using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorDeTarefas.Models.Tarefas
{
    class TarefaModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime Adicionado { get; set; }
        public DateTime? Finalizacao { get; set; }
        public DateTime? Previsao { get; set; }
        public Status Situacao { get; set; }
        public Prioridade Prioridade { get; set; }
    }
}
