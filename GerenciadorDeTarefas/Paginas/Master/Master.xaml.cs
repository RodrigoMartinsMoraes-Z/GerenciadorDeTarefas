using GerenciadorDeTarefas.Common.Models.Projects;
using GerenciadorDeTarefas.Common.Models.Teams;
using GerenciadorDeTarefas.Paginas.Equipes;
using GerenciadorDeTarefas.Paginas.Novidades;
using GerenciadorDeTarefas.Paginas.Projetos;
using GerenciadorDeTarefas.Paginas.Tarefas;

using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GerenciadorDeTarefas.Paginas.Master
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Master : MasterDetailPage
    {
        public static TeamModel EquipeSelecionada { get; set; }
        public static ProjectModel ProjetoSelecionado { get; set; }

        public Master()
        {
            InitializeComponent();
            Title = "Tarefeiro";
            Detail = new NavigationPage(new PaginaNovidades());

            AtualizarListaEquipes();

            AssinarMensagem();
        }

        private void AssinarMensagem()
        {
            MessagingCenter.Subscribe<Master>(this, "AtualizarMenu", (sender) => AtualizarListaEquipes());
        }

        public void AtualizarListaEquipes()
        {
            if (ListaEquipes.Children.Count > 0)
            {
                ListaEquipes.Children.Clear();
            }

            Button btnNovaEquipe = new Button { Text = "Nova Equipe" };
            btnNovaEquipe.Clicked += (sender, args) =>
            {
                Detail = new NavigationPage(new PaginaNovaEquipe());
                IsPresented = false;

            };

            ListaEquipes.Children.Add(btnNovaEquipe);

            CarregarEquipes(App.User.Teams);
        }

        private void CarregarEquipes(ICollection<TeamModel> equipes)
        {
            foreach (TeamModel equipe in App.User.Teams.OrderBy(e => e.Nome))
            {
                StackLayout layoutEquipe = new StackLayout() { IsVisible = false, Margin = 10 };
                Button btnMostrarEquipe = new Button
                {
                    Text = equipe.Nome
                };
                btnMostrarEquipe.Clicked += (sender, args) => ExibirDetalhes(layoutEquipe);
                ListaEquipes.Children.Add(btnMostrarEquipe);

                Button btnNovoProjeto = new Button
                {
                    Text = "Novo Projeto"
                };
                btnNovoProjeto.Clicked += (sender, args) =>
                {
                    PaginaNovoProjeto.Equipe = equipe;
                    Detail = new NavigationPage(new PaginaNovoProjeto());
                    IsPresented = false;
                };
                layoutEquipe.Children.Add(btnNovoProjeto);

                StackLayout layoutProjeto = new StackLayout();
                foreach (ProjectModel projeto in equipe.Projects.OrderBy(e => e.Name))
                {
                    Button btnMostrarProjeto = new Button
                    {
                        Text = projeto.Name
                    };
                    btnMostrarProjeto.Clicked += (object sender, EventArgs args) =>
                    {
                        PaginaTarefas.Projeto = projeto;
                        Detail = new NavigationPage(new PaginaTarefas());
                        IsPresented = false;
                    };

                    layoutProjeto.Children.Add(btnMostrarProjeto);
                }

                layoutEquipe.Children.Add(layoutProjeto);

                Button btnExcluirEquipe = new Button
                {
                    Text = "Excluir Equipe"
                };
                btnExcluirEquipe.Clicked += (sender, args) =>
                {
                    TeamModel Equipes = App.User.Teams.FirstOrDefault(e => e.Nome == equipe.Nome);
                    equipes.Remove(equipe);

                    //await App.Usuario.Salvar();
                    AtualizarListaEquipes();
                };
                layoutEquipe.Children.Add(btnExcluirEquipe);

                ListaEquipes.Children.Add(layoutEquipe);
            }
        }

        private void ExibirDetalhes(StackLayout stackLayout)
        {
            stackLayout.IsVisible = !stackLayout.IsVisible;
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