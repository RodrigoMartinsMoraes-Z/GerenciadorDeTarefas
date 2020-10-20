using GerenciadorDeTarefas.Domain.ManyToMany;
using GerenciadorDeTarefas.Domain.Projetos;

using System.Collections.Generic;

namespace GerenciadorDeTarefas.Domain.Equipes
{
    public class Equipe
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Projeto> Projetos { get; set; }
        public virtual ICollection<EquipeUsuario> Usuarios { get; set; }
    }
}
