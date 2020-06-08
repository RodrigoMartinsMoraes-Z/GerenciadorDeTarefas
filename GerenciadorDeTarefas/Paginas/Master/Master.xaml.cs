using GerenciadorDeTarefas.Models.Equipes;
using GerenciadorDeTarefas.Models.Tarefas;
using GerenciadorDeTarefas.Paginas.ListaDeTarefas;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GerenciadorDeTarefas.Paginas.Master
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Master : MasterDetailPage
    {
        public static EquipeModel EquipeSelecionada { get; set; }

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

        public Task AtualizarMenu()
        {
            if(ListaEquipes.Children.Count > 0)
            {
                ListaEquipes.Children.Clear();
            }

            foreach (EquipeModel equipe in App.Usuario.Equipes)
            {
                StackLayout layoutTarefas = new StackLayout() { IsVisible = false };

                Button BtnMostrarEquipe = new Button
                {
                    Text = equipe.Nome
                };
                BtnMostrarEquipe.Clicked += (sender, args) => MostrarEquipe(layoutTarefas, equipe);

                Button BtnNovaTarefa = new Button
                {
                    Text = "Nova Tarefa",
                };
                BtnNovaTarefa.Clicked += (sender, args) => Detail = new NavigationPage(new PaginaNovaTarefa());

                Picker picker = new Picker();
                if (equipe.Projetos != null && equipe.Projetos.Count > 0)
                    foreach (var tarefa in equipe.Projetos)
                    {
                        picker.Items.Add(tarefa.Nome);
                    }

                StackLayout layoutEquipes = new StackLayout() {
                    Orientation = StackOrientation.Vertical,
                    
                };
                
                layoutEquipes.Children.Add(BtnMostrarEquipe);
                layoutEquipes.Children.Add(layoutTarefas);

                layoutTarefas.Children.Add(BtnNovaTarefa);
                layoutTarefas.Children.Add(picker);

                ListaEquipes.Children.Add(layoutEquipes);
            }

            return Task.CompletedTask;
        }

        private void MostrarEquipe(StackLayout stackLayoutEquipe, EquipeModel equipe)
        {
            stackLayoutEquipe.IsVisible = !stackLayoutEquipe.IsVisible;
            EquipeSelecionada = equipe;
        }

        private void ChamaPaginaAFazer(object sender, EventArgs args)
        {
            Detail = new NavigationPage(new PaginaTarefasAFazer());
        }

        private void PaginaNovaTarefa(object sender, EventArgs args)
        {
            Detail = new NavigationPage(new PaginaNovaTarefa());
        }

        private void NovaEquipe(object sender, EventArgs args)
        {

        }

        private void RemoverTarefa(object sender, EventArgs args)
        {

        }

        private void RemoverEquipe(object sender, EventArgs args)
        {

        }

        private void ChamaPaginaPerfil(object sender, EventArgs args)
        {

        }

    }
}