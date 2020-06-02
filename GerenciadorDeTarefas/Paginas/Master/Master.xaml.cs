using GerenciadorDeTarefas.Models.Equipes;
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
        public Master()
        {
            InitializeComponent();

            List<EquipeModel> equipes = new List<EquipeModel>();
            equipes.Add(new EquipeModel { Nome = "Equipe 1" });
            equipes.Add(new EquipeModel { Nome = "Equipe 2" });

            foreach (EquipeModel equipe in equipes)
            {
                Picker picker = new Picker
                {
                    IsVisible = false
                };
                Button button = new Button();
                button.Text = equipe.Nome;
                button.Clicked += (sender, args) => picker.IsVisible = !picker.IsVisible;


                if (equipe.Tarefas != null && equipe.Tarefas.Count > 0)
                    foreach (var tarefa in equipe.Tarefas)
                    {
                        picker.Items.Add(tarefa.Nome);
                    }

                StackLayout stackLayout = new StackLayout();
                stackLayout.Children.Add(button);
                stackLayout.Children.Add(picker);
                ListaEquipes.Children.Add(stackLayout);
            }
        }

        private object ExibirPicker(int referencia)
        {
            throw new NotImplementedException();
        }

        private void ChamaPaginaAFazer(object sender, EventArgs args)
        {
            Detail = new NavigationPage(new PaginaTarefasAFazer());
        }

        private void ChamaPaginaPerfil(object sender, EventArgs args)
        {

        }

    }
}