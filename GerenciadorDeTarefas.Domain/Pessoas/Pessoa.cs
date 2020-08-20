using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorDeTarefas.Domain.Pessoas
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime Nascimento { get; set; }
        public string Email { get; set; }
    }
}
