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

        public string Nome { get; set; }
        public ICollection<Funcionalidade> Funcionalidades { get; set; }
        public ICollection<Tarefa> Tarefas { get; set; }
    }
}
