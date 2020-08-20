using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorDeTarefas.Domain.Tarefas
{
    public class Tarefa
    {
        public Tarefa()
        {
            SubTarefas = new List<Tarefa>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Detalhes { get; set; }
        public DateTime Adicionado { get; set; }
        public DateTime? Finalizacao { get; set; }
        public DateTime? Previsao { get; set; }
        public Situacao Situacao { get; set; }
        public Prioridade Prioridade { get; set; }

        public ICollection<Tarefa> SubTarefas { get; set; }
    }
}
