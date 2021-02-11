using GerenciadorDeTarefas.Common.Models.Usuarios;

using System;

namespace GerenciadorDeTarefas.Common.Models.Pessoas
{
    public class PessoaModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime Nascimento { get; set; }
        
        public virtual UsuarioModel Usuario { get; set; }
    }
}
