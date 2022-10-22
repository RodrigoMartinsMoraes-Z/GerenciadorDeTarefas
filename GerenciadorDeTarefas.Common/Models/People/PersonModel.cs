using GerenciadorDeTarefas.Common.Models.Usuarios;

using System;

namespace GerenciadorDeTarefas.Common.Models.Pessoas
{
    public class PersonModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birth { get; set; }

        public virtual UserModel User { get; set; }
    }
}
