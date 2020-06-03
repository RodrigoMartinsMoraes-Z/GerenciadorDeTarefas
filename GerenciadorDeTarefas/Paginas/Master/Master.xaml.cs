using GerenciadorDeTarefas.Models.Equipes;
using GerenciadorDeTarefas.Models.Tarefas;
using GerenciadorDeTarefas.Paginas.ListaDeTarefas;
using System;
using System.Collections.Generic;
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

#if DEBUG
            if (App.Usuario == null)
            {
                List<TarefaModel> tarefas1 = new List<TarefaModel>();
                tarefas1.Add(new TarefaModel { Nome = "aa1" });
                tarefas1.Add(new TarefaModel { Nome = "aa2" });

                List<TarefaModel> tarefas2 = new List<TarefaModel>();
                tarefas2.Add(new TarefaModel { Nome = "bb1" });
                tarefas2.Add(new TarefaModel { Nome = "bb2" });

                List<EquipeModel> equipes = new List<EquipeModel>
            {
                new EquipeModel { Nome = "Equipe 1", Tarefas =  tarefas1},
                new EquipeModel { Nome = "Equipe 2", Tarefas = tarefas2 }
            };

                App.Usuario.Equipes = equipes;
                App.Usuario.Salvar();
            }
#endif
            foreach (EquipeModel equipe in App.Usuario.Equipes)
            {
                StackLayout stackLayoutEquipe = new StackLayout() { IsVisible = false };

                Button BtnMostrarEquipe = new Button
                {
                    Text = equipe.Nome
                };
                BtnMostrarEquipe.Clicked += (sender, args) => MostrarEquipe(stackLayoutEquipe, equipe);

                Button BtnNovaTarefa = new Button
                {
                    Text = "Nova Tarefa",
                };
                BtnNovaTarefa.Clicked += (sender, args) => Detail = new NavigationPage(new PaginaNovaTarefa());

                Picker picker = new Picker();
                if (equipe.Tarefas != null && equipe.Tarefas.Count > 0)
                    foreach (var tarefa in equipe.Tarefas)
                    {
                        picker.Items.Add(tarefa.Nome);
                    }

                StackLayout stackLayout = new StackLayout();
                stackLayout.Children.Add(BtnMostrarEquipe);
                stackLayout.Children.Add(stackLayoutEquipe);

                stackLayoutEquipe.Children.Add(BtnNovaTarefa);
                stackLayoutEquipe.Children.Add(picker);
                ListaEquipes.Children.Add(stackLayout);
            }
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