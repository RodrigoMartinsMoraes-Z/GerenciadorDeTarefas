using GerenciadorDeTarefas.Domain.Equipes;
using GerenciadorDeTarefas.Domain.Objetivos;
using GerenciadorDeTarefas.Domain.Tarefas;

using System.Collections.Generic;

namespace GerenciadorDeTarefas.Domain.Projetos
{
    public class Projeto
    {
        public Projeto()
        {
            Funcionalidades = new List<Objetivo>();
            Tarefas = new List<Tarefa>();
        }
        public int Id { get; set; }
        public int IdProjeto { get; set; }
        public string Nome { get; set; }

        public virtual Equipe Equipe { get; set; }
        public virtual ICollection<Objetivo> Funcionalidades { get; set; }
        public virtual ICollection<Tarefa> Tarefas { get; set; }
    }
}
