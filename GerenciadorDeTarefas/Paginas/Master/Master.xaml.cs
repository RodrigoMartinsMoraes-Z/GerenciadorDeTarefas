using GerenciadorDeTarefas.Common.Models.Equipes;
using GerenciadorDeTarefas.Common.Models.Projetos;
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
        public static EquipeModel EquipeSelecionada { get; set; }
        public static ProjetoModel ProjetoSelecionado { get; set; }

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

            CarregarEquipes(App.Usuario.Equipes);
        }

        private void CarregarEquipes(ICollection<EquipeModel> equipes)
        {
            foreach (EquipeModel equipe in App.Usuario.Equipes.OrderBy(e => e.Nome))
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
                foreach (ProjetoModel projeto in equipe.Projetos.OrderBy(e => e.Nome))
                {
                    Button btnMostrarProjeto = new Button
                    {
                        Text = projeto.Nome
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
                btnExcluirEquipe.Clicked += async (sender, args) =>
                {
                    EquipeModel Equipes = App.Usuario.Equipes.FirstOrDefault(e => e.Nome == equipe.Nome);
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