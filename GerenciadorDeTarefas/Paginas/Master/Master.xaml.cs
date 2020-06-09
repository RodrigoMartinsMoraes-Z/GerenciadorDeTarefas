using GerenciadorDeTarefas.Models.Equipes;
using GerenciadorDeTarefas.Models.Projetos;
using GerenciadorDeTarefas.Models.Tarefas;
using GerenciadorDeTarefas.Models.Usuarios;
using GerenciadorDeTarefas.Paginas.ListaDeTarefas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GerenciadorDeTarefas.Paginas.Master
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Master : MasterDetailPage
    {
        public static EquipeModel EquipeSelecionada { get; set; }
        public static ProjetoModel ProjetoSelecionado { get; set; }

        public Master()
        {
            InitializeComponent();

            AtualizarMenu();

            AssinarMensagem();
        }

        private void AssinarMensagem()
        {
            MessagingCenter.Subscribe<Master>(this, "AtualizarMenu", (sender) => AtualizarMenu());
        }

        public async Task AtualizarMenu()
        {
            if (ListaEquipes.Children.Count > 0)
            {
                ListaEquipes.Children.Clear();
            }

            await CarregarEquipes(App.Usuario.Equipes);
        }

        private async Task CarregarEquipes(ICollection<EquipeModel> equipes)
        {
            foreach (EquipeModel equipe in App.Usuario.Equipes)
            {
                StackLayout layoutEquipes = new StackLayout() { IsVisible = false, Margin = 10 };
                Button btnMostrarEquipe = new Button
                {
                    Text = equipe.Nome
                };
                btnMostrarEquipe.Clicked += (sender, args) => ExibirDetalhes(layoutEquipes, equipe);

                await CarregarProjetos(layoutEquipes, equipe.Projetos);

                ListaEquipes.Children.Add(btnMostrarEquipe);

                Button btnExcluirEquipe = new Button
                {
                    Text = "Excluir Equipe"
                };
                btnExcluirEquipe.Clicked += (sender, args) =>
                {
                    var Equipes = App.Usuario.Equipes.FirstOrDefault(e => e.Nome == equipe.Nome);
                    equipes.Remove(equipe);

                    App.Usuario.Salvar();
                    AtualizarMenu();
                };
                layoutEquipes.Children.Add(btnExcluirEquipe);

                ListaEquipes.Children.Add(layoutEquipes);
            }
        }

        private async Task CarregarProjetos(StackLayout layoutEquipes, ICollection<ProjetoModel> projetos)
        {
            foreach (ProjetoModel projeto in projetos)
            {
                StackLayout layoutProjeto = new StackLayout() { IsVisible = false, Margin = 10 };
                Button btnMostrarProjeto = new Button
                {
                    Text = projeto.Nome
                };
                btnMostrarProjeto.Clicked += (sender, args) => Detail = new NavigationPage(new PaginaTarefas());

                layoutProjeto.Children.Add(btnMostrarProjeto);
                layoutProjeto.Children.Add(layoutProjeto);

                Button btnExcluirProjeto = new Button
                {
                    Text = "Excluir Projeto"
                };
                btnExcluirProjeto.Clicked += (sender, args) =>
                {
                    var projetosDaEquipe = App.Usuario.Equipes.FirstOrDefault(e => e.Projetos == projetos).Projetos;
                    projetosDaEquipe.Remove(projeto);

                    App.Usuario.Salvar();
                    AtualizarMenu();
                };
                layoutProjeto.Children.Add(btnExcluirProjeto);

                layoutEquipes.Children.Add(layoutProjeto);
            }

            await Task.CompletedTask;
        }

        private void ExibirDetalhes(StackLayout stackLayout, EquipeModel equipe = null, ProjetoModel projeto = null)
        {
            stackLayout.IsVisible = !stackLayout.IsVisible;
            EquipeSelecionada = equipe;
        }

        private void PaginaNovaTarefa(object sender, EventArgs args)
        {
            Detail = new NavigationPage(new PaginaNovaTarefa());
        }

        private void PaginaNovaEquipe(object sender, EventArgs args)
        {
            
        }

        private void ChamaPaginaPerfil(object sender, EventArgs args)
        {

        }

        private void ExibirEquipes(object sender, EventArgs args)
        {
            ListaEquipes.IsVisible = !ListaEquipes.IsVisible;
        }
    }
}