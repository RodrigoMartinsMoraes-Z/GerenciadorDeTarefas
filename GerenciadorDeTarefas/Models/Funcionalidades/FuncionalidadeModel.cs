using GerenciadorDeTarefas.Models.Tarefas;
using System;
using System.Collections.Generic;

namespace GerenciadorDeTarefas.Models.Funcionalidades
{
    public class FuncionalidadeModel
    {
        public string Nome { get; set; }
        public string Detalhes { get; set; }
        public DateTime Adicionado { get; set; }
        public DateTime? Finalizacao { get; set; }
        public DateTime? Previsao { get; set; }
        public Situacao Situacao { get; set; }
        public Prioridade Prioridade { get; set; }

        public ICollection<TarefaModel> Tarefas { get; set; }
    }
}
