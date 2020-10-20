using GerenciadorDeTarefas.Domain.Projetos;
using GerenciadorDeTarefas.Domain.Tarefas;

using System;
using System.Collections.Generic;

namespace GerenciadorDeTarefas.Domain.Funcionalidades
{
    public class Objetivo
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

        public virtual Projeto Projeto { get; set; }
        public virtual ICollection<Tarefa> Tarefas { get; set; }
    }
}
