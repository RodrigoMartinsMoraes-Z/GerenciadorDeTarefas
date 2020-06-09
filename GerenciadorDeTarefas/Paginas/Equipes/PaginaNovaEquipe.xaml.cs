using GerenciadorDeTarefas.Models.Equipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GerenciadorDeTarefas.Paginas.Equipes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaNovaEquipe : ContentPage
    {
        public PaginaNovaEquipe()
        {
            InitializeComponent();

            Title = "Nova Equipe";
            
        }

        public void AdicionarEquipe (object sender, EventArgs args)
        {
            EquipeModel novaEquipe = new EquipeModel { Nome = NomeEquipe.Text };

            App.Usuario.Equipes.Add(novaEquipe);
            App.Usuario.Salvar();

            ControleMenu.AtualizarMenu();
        }
    }
}