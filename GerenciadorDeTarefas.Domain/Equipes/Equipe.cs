using GerenciadorDeTarefas.Domain.Projetos;
using GerenciadorDeTarefas.Domain.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorDeTarefas.Domain.Equipes
{
    public class Equipe
    {
        public Equipe()
        {
            Projetos = new List<Projeto>();
            Usuarios = new List<Usuario>();
        }

        public string Nome { get; set; }
        public ICollection<Projeto> Projetos { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
