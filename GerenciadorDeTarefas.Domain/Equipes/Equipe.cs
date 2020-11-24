using GerenciadorDeTarefas.Domain.ManyToMany;
using GerenciadorDeTarefas.Domain.Projetos;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.Domain.Equipes
{
    public class Equipe
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Projeto> Projetos { get; set; }
        public virtual ICollection<EquipeUsuario> Usuarios { get; set; }

        public Task Validate()
        {
            if (string.IsNullOrEmpty(Nome) || string.IsNullOrWhiteSpace(Nome))
                throw new System.Exception("O campo 'Nome' deve ser preenchido.");

            return Task.CompletedTask;
        }
    }
}
