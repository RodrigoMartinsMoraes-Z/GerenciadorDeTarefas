using FontAwesome;
using GerenciadorDeTarefas.Models.Projetos;
using GerenciadorDeTarefas.Models.Tarefas;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GerenciadorDeTarefas.Paginas.Tarefas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaTarefas : ContentPage
    {
        public static ProjetoModel Projeto { get; set; }

        public PaginaTarefas()
        {
            InitializeComponent();

            if (Projeto == null)
                DisplayAlert("Erro!", "Não foi possivel carregar as informações sobre o projeto selecionado, favor tentar novamente", "OK");
            else
                Task.WaitAny(
                    CarregarTarefas()
                    );
        }

        private async Task CarregarTarefas()
        {
            Title = Projeto.Nome;

            int linha = 0;

            Grid grid = new Grid();

            foreach (TarefaModel tarefa in Projeto.Tarefas)
            {
                List<View> views = await ListView(tarefa, layoutTarefas);
                //layoutTarefas.Children.Add(stackLayout);

                //ICollection coluna = await GerarColuna(tarefa);
                int coluna = 0;
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });

                foreach (View item in views)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });

                    grid.Children.Add(item, coluna, linha);
                    coluna++;
                }
                linha++;
            }

            layoutTarefas.Children.Add(grid);

        }

        //private Task<ICollection> GerarColuna(TarefaModel tarefa)
        //{
        //    var label = new Label
        //    {
        //        Text = tarefa.Nome
        //    };

        //    var button = new Button
        //    {
        //        Text = FontAwesomeIcons.Eye,
        //        FontFamily = App.FontAwesomeSolid
        //    };

        //    ICollection<(Label,Button)> itens = new ICollection<(Label, Button)>();

        //    return;
        //}

        private async Task<List<View>> ListView(TarefaModel tarefa, StackLayout layoutTarefas)
        {
            List<View> views = new List<View>
            {
                new Label
                {
                    Text = tarefa.Nome
                },
                new Button
                {
                    Text = FontAwesomeIcons.Eye,
                    FontFamily = App.FontAwesomeSolid
                }
            };


            await Task.CompletedTask;

            //if (tarefa.SubTarefas != null && tarefa.SubTarefas.Count > 0)
            //{
            //    foreach (TarefaModel tarefa2 in tarefa.SubTarefas)
            //    {
            //        StackLayout layoutSubTarefa = await CriarLayoutDaTarefa(tarefa2, layout);
            //        layout.Children.Add(layoutSubTarefa);
            //    }
            //}


            return views;
        }

        private void AddNovaTarefa(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PaginaNovaTarefa());
            PaginaNovaTarefa.Projeto = Projeto;
        }
    }
}