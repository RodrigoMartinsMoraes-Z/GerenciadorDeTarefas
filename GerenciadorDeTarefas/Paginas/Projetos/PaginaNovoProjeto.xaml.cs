using GerenciadorDeTarefas.Common.Models.Projects;
using GerenciadorDeTarefas.Common.Models.Teams;

using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GerenciadorDeTarefas.Paginas.Projetos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaNovoProjeto : ContentPage
    {
        private readonly IControleMenu _controleMenu = App.IoCConainer.GetInstance<IControleMenu>();
        public static TeamModel Equipe { get; set; }

        public PaginaNovoProjeto()
        {
            InitializeComponent();
        }

        private void AdicionarProjeto(object sender, EventArgs args)
        {
            ProjectModel projeto = new ProjectModel
            {
                Name = NomeProjeto.Text
            };

            TeamModel equipe = App.User.Teams.SingleOrDefault(e => e.Nome == Equipe.Nome);
            if (equipe != null)
            {
                equipe.Projects.Add(projeto);
                App.User.Teams.Remove(equipe);
                App.User.Teams.Add(equipe);
                //App.Usuario.Salvar();
                _controleMenu.AtualizarListaEquipes();
            }
            else
            {
                DisplayAlert("Ooops", "Algo de errado não está certo, por favor, reinicie o aplicativo.", "OK");
            }
        }

        private void NomeProjeto_TextChanged(object sender, TextChangedEventArgs e)
        {
            NomeProjetoLbl.IsVisible = true;
        }
    }
}