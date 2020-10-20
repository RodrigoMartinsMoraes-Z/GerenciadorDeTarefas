using GerenciadorDeTarefas.Domain.Usuarios;

using System;

namespace GerenciadorDeTarefas.Domain.Pessoas
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime Nascimento { get; set; }
        public string Email { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
