using GerenciadorDeTarefas.Domain.Equipes;
using GerenciadorDeTarefas.Domain.Funcionalidades;
using GerenciadorDeTarefas.Domain.Tarefas;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorDeTarefas.Domain.Projetos
{
    public class Projeto
    {
        public Projeto()
        {
            Funcionalidades = new List<Funcionalidade>();
            Tarefas = new List<Tarefa>();
        }
        public int Id { get; set; }
        public string Nome { get; set; }

        public virtual Equipe Equipe { get; set; }
        public virtual ICollection<Funcionalidade> Funcionalidades { get; set; }
        public virtual ICollection<Tarefa> Tarefas { get; set; }
    }
}
