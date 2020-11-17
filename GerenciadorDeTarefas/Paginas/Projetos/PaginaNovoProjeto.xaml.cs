
using GerenciadorDeTarefas.Common.Models.Equipes;
using GerenciadorDeTarefas.Common.Models.Projetos;

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
        public static EquipeModel Equipe { get; set; }

        public PaginaNovoProjeto()
        {
            InitializeComponent();
        }

        private void AdicionarProjeto(object sender, EventArgs args)
        {
            ProjetoModel projeto = new ProjetoModel
            {
                Nome = NomeProjeto.Text
            };

            EquipeModel equipe = App.Usuario.Equipes.SingleOrDefault(e => e.Nome == Equipe.Nome);
            if (equipe != null)
            {
                equipe.Projetos.Add(projeto);
                App.Usuario.Equipes.Remove(equipe);
                App.Usuario.Equipes.Add(equipe);
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