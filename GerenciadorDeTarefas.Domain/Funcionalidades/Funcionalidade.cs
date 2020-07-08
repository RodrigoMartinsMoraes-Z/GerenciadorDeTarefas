using GerenciadorDeTarefas.Domain.Tarefas;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorDeTarefas.Domain.Funcionalidades
{
    public class Funcionalidade
    {
        public string Nome { get; set; }
        public string Detalhes { get; set; }
        public DateTime Adicionado { get; set; }
        public DateTime? Finalizacao { get; set; }
        public DateTime? Previsao { get; set; }
        public Situacao Situacao { get; set; }
        public Prioridade Prioridade { get; set; }

        public ICollection<Tarefa> Tarefas { get; set; }
    }
}
