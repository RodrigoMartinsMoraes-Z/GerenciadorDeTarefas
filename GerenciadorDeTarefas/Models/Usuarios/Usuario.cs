using GerenciadorDeTarefas.Models.Equipes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.Models.Usuarios
{
    public class Usuario
    {
        public Usuario()
        {
            Equipes = new List<EquipeModel>();
        }

        public ICollection<EquipeModel> Equipes { get; set; }

        public Task Salvar()
        {
            string json = JsonConvert.SerializeObject(this);

            if (App.Current.Properties.ContainsKey("Usuario"))
                App.Current.Properties.Remove("Usuario");

            App.Current.Properties.Add("Usuario", json);

            return Task.CompletedTask;
        }
    }
}
